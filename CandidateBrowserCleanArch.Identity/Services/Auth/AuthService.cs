using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Identity.Helpers;
using CandidateBrowserCleanArch.Identity.Interfaces;
using System.Security.Claims;

namespace CandidateBrowserCleanArch.Identity.Services.Auth;

internal class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserServicesManager _userServicesManager;
    private readonly IExternalAuthProvidersValidator _externalAuthProvidersValidator;
    private readonly IGoogleAuthHelper _googleAuthHelper;

    public AuthService(IJwtService jwtService,
        IUserServicesManager userServicesManager,
        IExternalAuthProvidersValidator externalAuthProvidersValidator,
        IGoogleAuthHelper googleAuthHelper
)
    {
        _jwtService = jwtService;
        _userServicesManager = userServicesManager;
        _externalAuthProvidersValidator = externalAuthProvidersValidator;
        _googleAuthHelper = googleAuthHelper;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var response = new AuthResponse();

        var validationAttempt = await _userServicesManager.ValidateAndLoginUserAsync(request.Email, request.Password);
        if (validationAttempt.user == null)
        {
            response.Message = validationAttempt.validationMessage;
            return response;
        }
        var user = validationAttempt.user;
        user.RefreshToken = _jwtService.GenerateRefreshToken();

        await _userServicesManager.UpdateUser(user);
        var userClaims = await _userServicesManager.GatherUserClaims(user);

        response.Token = _jwtService.GenerateToken(user, userClaims);
        response.RefreshToken = user.RefreshToken;
        response.Success = true;

        return response;
    }

    public async Task<AuthResponse> AuthWithGoogle(string authCode, string redirectUrl)
    {
        var response = new AuthResponse();

        var token = await _googleAuthHelper.GetAccessTokenAsync(authCode, redirectUrl);

        var validationResult = await _externalAuthProvidersValidator.ValidateGoogleToken(token);
        if (validationResult.user is null)
        {
            response.Message = validationResult.message;
            return response;
        }
        var result = await _userServicesManager.ValidateExternalProviderUserAsync(validationResult.user);
        if (result.user == null)
        {
            response.Message = string.Join(":", result.validationMessages);
            return response;
        }

        result.user.RefreshToken = _jwtService.GenerateRefreshToken();

        await _userServicesManager.UpdateUser(result.user);
        var userClaims = await _userServicesManager.GatherUserClaims(result.user);

        response.Token = _jwtService.GenerateToken(result.user, userClaims);
        response.RefreshToken = result.user.RefreshToken;
        response.Success = true;

        return response;
    }

    public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
    {
        var response = new AuthResponse();

        var principal = _jwtService.GetPrincipalFromExpiredToken(request.Token);
        var validationRequest = await _userServicesManager.ValidateUserAndToken(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
            request.RefreshToken);
        if (validationRequest.user == null)
        {
            response.Message = validationRequest.validationMessage;
            return response;
        }
        var user = validationRequest.user;
        var userClaims = await _userServicesManager.GatherUserClaims(user);

        response.Token = _jwtService.GenerateToken(user, userClaims);
        user.RefreshToken = _jwtService.GenerateRefreshToken();
        await _userServicesManager.UpdateUser(user);

        response.RefreshToken = user.RefreshToken;
        response.Success = true;

        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email
        };

        var response = new RegistrationResponse();

        var result = await _userServicesManager.ValidateAndRegisterUserAsync(user, request.Password);
        if (result.user == null)
        {
            result.validationMessages.ToList().ForEach(error => response.Errors.Add(error));
            return response;
        }
        response.UserId = user.Id;

        var tokenAttempt = await _userServicesManager.CreateConfirmEmailToken(user);
        if (!string.IsNullOrEmpty(tokenAttempt.encryptedEmailToken))
        {
            response.Success = true;
            response.Message = $"{user.Email} registration success";
            response.ValidToken = tokenAttempt.encryptedEmailToken;
            response.EncryptedUserId = tokenAttempt.encryptedUserId;
        }
        else
        {
            response.Message = $"{tokenAttempt.validationMessage}";
        }
        return response;
    }

    public async Task<ServiceReponse<string>> GetGoogleAuthUrl(string redirectUrl)
    {
        var response = new ServiceReponse<string>();
        try
        {
            response.Data = _googleAuthHelper.GetGoogleUrl(redirectUrl);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Data = redirectUrl;
            response.Success = false;
            response.Message = ex.Message;

        }
        return response;

    }

    public async Task<ServiceReponse<bool>> ConfirmEmail(ConfirmEmailRequest request)
    {
        var response = new ServiceReponse<bool>();
        response.Success = await _userServicesManager.ValidateAndConfirmEmail(request.UserId, request.Token);
        return response;
    }

    public async Task<ConfirmEmailRepeatResponse> ConfirmEmailRepeat(ConfirmEmailRepeatRequest request)
    {
        var response = new ConfirmEmailRepeatResponse();
        var tokenAttempt = await _userServicesManager.CreateConfirmEmailToken(request.Email);
        if (!string.IsNullOrEmpty(tokenAttempt.encryptedEmailToken))
        {
            response.Success = true;
            response.ValidToken = tokenAttempt.encryptedEmailToken;
            response.EncryptedUserId = tokenAttempt.encryptedUserId;
        }
        else
        {
            response.Message = $"{tokenAttempt.validationMessage}";
        }
        return response;

    }

    public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request)
    {
        var response = new ForgotPasswordResponse();
        var tokenAttempt = await _userServicesManager.CreateResetPasswordToken(request.Email);
        if (!string.IsNullOrEmpty(tokenAttempt.encryptedPasswordToken))
        {
            response.Success = true;
            response.ValidToken = tokenAttempt.encryptedPasswordToken;
            response.EncryptedUserId = tokenAttempt.encryptedUserId;
        }
        else
        {
            response.Message = $"{tokenAttempt.validationMessage}";
        }

        return response;
    }

    public async Task<ServiceReponse<bool>> ResetPassword(ResetPasswordRequest request)
    {
        var response = new ServiceReponse<bool>();
        var result = await _userServicesManager.ValidateAndResetPassword(request.UserId, request.Token, request.NewPassword);
        response.Success = result.result;
        response.Message = result.message;

        return response;
    }
}


