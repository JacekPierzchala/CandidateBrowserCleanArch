using Google.Apis.Auth;

namespace CandidateBrowserCleanArch.Identity;

internal class ExternalAuthProvidersValidator : IExternalAuthProvidersValidator
{
    public async Task<(ApplicationUser? user, string message)> ValidateGoogleToken(string token)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(token);
        if (!payload.EmailVerified
            && DateTime.MinValue.AddSeconds((double)payload.ExpirationTimeSeconds) > DateTime.UtcNow)
        {
            return (null,"Invalid google token");
        }
        var user = new ApplicationUser
        {
            Email = payload.Email,
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            UserName = payload.Email
        };
        return (user, string.Empty);
    }
}