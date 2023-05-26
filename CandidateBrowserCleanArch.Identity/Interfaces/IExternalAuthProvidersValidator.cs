namespace CandidateBrowserCleanArch.Identity.Interfaces;

internal interface IExternalAuthProvidersValidator
{
    Task<(ApplicationUser? user, string message)> ValidateGoogleToken(string token);
}
