namespace CandidateBrowserCleanArch.Identity.Helpers;

internal static class ExternalAuthUrlStatics
{
    internal const string _authEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    internal const string _tokenEndpoint = "https://oauth2.googleapis.com/token";
    internal const string _googleScopes = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile openid";

}