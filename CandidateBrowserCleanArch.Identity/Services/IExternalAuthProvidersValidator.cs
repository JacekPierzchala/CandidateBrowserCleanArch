namespace CandidateBrowserCleanArch.Identity;

internal interface IExternalAuthProvidersValidator
{
    Task<(ApplicationUser? user, string message)> ValidateGoogleToken(string token);
}
