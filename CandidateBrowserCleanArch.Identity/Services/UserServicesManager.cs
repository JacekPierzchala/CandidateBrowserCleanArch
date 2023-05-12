using CandidateBrowserCleanArch.Application;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CandidateBrowserCleanArch.Identity;
internal class UserServicesManager: IUserServicesManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEncryptService _encryptService;


    public UserServicesManager(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IEncryptService encryptService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _encryptService = encryptService;
    }

    public async Task<(ApplicationUser? user, string validationMessage)>ValidateUserAndToken(string username, string refreshToken)
    {
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return (null, "Invalid token refresh request");
        }
        return (user!, null);
    }

    public async Task UpdateUser(ApplicationUser user)
                 => await _userManager.UpdateAsync(user);
   
    public async Task<(ApplicationUser? user, string validationMessage)> ValidateAndLoginUserAsync(string userName, string password)
    {
        var user = await _userManager.FindByEmailAsync(userName);
        if (user == null)
        {
            return (null, $"Username '{userName}' does not exists");
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
        if (!result.Succeeded)
        {
            return (null, $"Invalid credentials");
        }
        user.DateLogged = DateTime.UtcNow;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        return (user, string.Empty);
    }

    public async Task<(ApplicationUser? user, IList<string>validationMessages)> ValidateAndRegisterUserAsync
        (ApplicationUser user, string password)
    {
        List<string> validationMessages = new();
        var existingUser = await _userManager.FindByEmailAsync(user.Email);
        if (existingUser != null)
        {
            validationMessages.Add($"Username '{user.Email}' already exists");
            return (null, validationMessages);
        }

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(error => validationMessages.Add(error.Description));
            return (null, validationMessages);
        }
        await _userManager.AddToRoleAsync(user, "User");
        return (user, validationMessages);
    }

    public async Task<IEnumerable<Claim>> GatherUserClaims(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            roleClaims.AddRange(
                await _roleManager.GetClaimsAsync(
                    await _roleManager.FindByNameAsync(role)
                    ));
        }

        var rolePermissions = new List<Claim>();
        for (int i = 0; i < roles.Count; i++)
        {
            rolePermissions.Add(new Claim(ClaimTypes.Role, roles[i]));
        }
        foreach (var role in roleClaims.Select(c => c.Value).Distinct().ToList())
        {
            rolePermissions.Add(new Claim(CustomClaimTypes.Permission, role));
        }       
        return roleClaims.Union(rolePermissions);
    }

    public async Task<(ApplicationUser? user, IList<string> validationMessages)> ValidateExternalProviderUserAsync(ApplicationUser user)
    {
        List<string> validationMessages = new();
        var existingUser = await _userManager.FindByEmailAsync(user.Email);

        if (existingUser == null) 
        {
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error => validationMessages.Add(error.Description));
                return (null, validationMessages);
            }
            await _userManager.AddToRoleAsync(user, "User");           
        }
 
        await _signInManager.SignInAsync(existingUser??user, false);
        if(existingUser!=null)
        {
            existingUser.DateLogged = DateTime.UtcNow;
            existingUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        }
        else
        {
            user.DateLogged = DateTime.UtcNow;
            user.RefreshTokenExpiryTime=DateTime.UtcNow.AddDays(7);
        }
        return (existingUser ??user, null);
    }

    public async Task<(string encryptedEmailToken,string encryptedUserId, string validationMessage)> CreateConfirmEmailToken(ApplicationUser user)
    {
        try
        {
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
           
            var encryptedEmailToken = await _encryptService.EncryptAsync(confirmEmailToken);
            var encryptedUserId = await _encryptService.EncryptAsync(user.Id);
            return (encryptedEmailToken, encryptedUserId, string.Empty);
        }
        catch (Exception ex)
        {
            return (string.Empty,string.Empty, ex.Message);
        }      
    }

    public async Task<(string encryptedEmailToken, string encryptedUserId, string validationMessage)> CreateConfirmEmailToken(string email)
    {
        var user =await _userManager.FindByEmailAsync(email);
        if(user!=null)
        {
            return await CreateConfirmEmailToken(user);
        }
        return (string.Empty, string.Empty, $"User {email} not found");
    }


    public async Task<bool> ValidateAndConfirmEmail(string userId, string token)
    {
        try
        {
            var decrypteduserId=await _encryptService.DecryptAsync(userId);
            var user = await _userManager.FindByIdAsync(decrypteduserId);
            if (user != null)
            {
                var decryptedToken = await _encryptService.DecryptAsync(token);
                var result = await _userManager.ConfirmEmailAsync(user, decryptedToken);
          
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }

    }

    public async Task<(string encryptedPasswordToken, string encryptedUserId, string validationMessage)> CreateResetPasswordToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var confirmPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encryptedPasswordToken = await _encryptService.EncryptAsync(confirmPasswordToken);
            var encryptedUserId = await _encryptService.EncryptAsync(user.Id);
            return (encryptedPasswordToken, encryptedUserId, string.Empty);
        }
        return (string.Empty, string.Empty, $"User {email} not found");
    }

    public async Task<(bool result,string message)> ValidateAndResetPassword(string userId, string token, string newPassword)
    {
        try
        {
            var decrypteduserId = await _encryptService.DecryptAsync(userId);
            var user = await _userManager.FindByIdAsync(decrypteduserId);
            if (user != null)
            {
                var decryptedToken = await _encryptService.DecryptAsync(token);
                var result = await _userManager.ResetPasswordAsync(user, decryptedToken, newPassword);              
                if (result.Succeeded)
                {
                    return (true,string.Empty);
                }

            }
            return  (false, "Reset password failed. Please resend request using forgot password module");
        }
        catch(Exception ex)
        {
            return  (false, ex.Message); 
        }
    }
}


