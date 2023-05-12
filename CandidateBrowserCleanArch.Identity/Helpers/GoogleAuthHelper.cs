using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity.Helpers;

internal class GoogleAuthHelper : IGoogleAuthHelper
{
    private readonly IConfiguration _configuration;

    public GoogleAuthHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GetGoogleUrl(string redirectUri)
    {

        string challenge = string.Empty;
        using (var sha256 = SHA256.Create())
        {
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_configuration["Authentication:Google:CodeVerifier"]));
            challenge = Base64Url.Encode(challengeBytes);
        }

        var parameters = new Dictionary<string, string>
    {
                { "client_id", _configuration["Authentication:Google:ClientId"] },
                { "redirect_uri", redirectUri },
                { "scope", ExternalAuthUrlStatics._googleScopes},
                { "response_type", "code" },
                { "access_type","offline"},
                { "prompt","select_account"},
                { "code_challenge", challenge },
                { "code_challenge_method", "S256" },
    };

        var url = QueryHelpers.AddQueryString(ExternalAuthUrlStatics._authEndpoint, parameters);
        return url;
    }

    public async Task<string> GetAccessTokenAsync(string authCode, string redirectUri)
    {

        var googleToken = new GoogleToken() { IdToken = "" };
        var parameters = new Dictionary<string, string>
        {

                    { "client_id", _configuration["Authentication:Google:ClientId"]  },
                    { "client_secret", _configuration["Authentication:Google:ClientSecret"]  },
                    { "redirect_uri", redirectUri },
                    { "code", authCode },
                    { "code_verifier", _configuration["Authentication:Google:CodeVerifier"]  },
                    { "grant_type", "authorization_code" },

        };

        var request = new HttpRequestMessage(HttpMethod.Post, ExternalAuthUrlStatics._tokenEndpoint);
        request.SetBrowserRequestMode(BrowserRequestMode.NoCors);
        request.SetBrowserRequestCache(BrowserRequestCache.NoStore);
        using (var httpClient = new HttpClient())
        {

            var res = await httpClient.PostAsJsonAsync(request.RequestUri, parameters);
            var contentStream = await res.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            JsonSerializer serializer = new();
            try
            {
                googleToken = serializer.Deserialize<GoogleToken>(jsonReader);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Invalid JSON.");
            }
        }

        return googleToken?.IdToken;
    }

}
