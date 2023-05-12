namespace CandidateBrowserCleanArch.Identity.Helpers
{
    internal interface IGoogleAuthHelper
    {
        Task<string> GetAccessTokenAsync(string authCode, string redirectUri);
        string GetGoogleUrl(string redirectUri);
    }
}