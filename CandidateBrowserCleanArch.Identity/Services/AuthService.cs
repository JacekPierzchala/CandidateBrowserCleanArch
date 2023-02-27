using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity;

internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService,
        RoleManager<IdentityRole> roleManager)

    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _roleManager = roleManager;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var response = new AuthResponse();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            response.Message = $"Username '{request.Email}' does not exists";
            return response;
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
        if (!result.Succeeded)
        {
            response.Message = $"Credentials for '{request.Email}' arent valid";
            return response;
        }
        user.DateLogged = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims= new List<Claim>();
        foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var roleClaimsToAdd = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(role));
            roleClaims.AddRange(roleClaimsToAdd);
        }
        response.Token = _jwtService.GenerateToken(userClaims, roles, user, roleClaims.Select(c => c.Value).Distinct().ToList());
        response.Success = true;

        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var response = new RegistrationResponse();
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            response.Errors.Add($"Username '{request.Email}' already exists");
            return response;
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true,
            UserName = request.Email
        };
 
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(error => response.Errors.Add(error.Description));            
            return response;
        }

        await _userManager.AddToRoleAsync(user, "User");
        response.Success = true;
        response.Message = $"{user.Email} registration success";
        response.UserId = user.Id;
        return response;
    }

}


