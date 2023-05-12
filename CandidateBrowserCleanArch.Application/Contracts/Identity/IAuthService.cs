using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
    Task<AuthResponse> AuthWithGoogle(string authCode, string redirectUrl);
    Task<AuthResponse> RefreshToken(RefreshTokenRequest request);
    Task<ServiceReponse<string>> GetGoogleAuthUrl(string redirectUrl);
    Task<ServiceReponse<bool>> ConfirmEmail(ConfirmEmailRequest request);

    Task<ConfirmEmailRepeatResponse> ConfirmEmailRepeat(ConfirmEmailRepeatRequest request);
    Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);

    Task<ServiceReponse<bool>> ResetPassword(ResetPasswordRequest request);
}
