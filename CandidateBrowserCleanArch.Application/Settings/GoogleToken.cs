﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GoogleToken
{
    //[JsonProperty("access_token")]
    public string AccessToken { get; set; }

   // [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }

   // [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    //[JsonProperty("scope")]
    public string Scope { get; set; }

    //[JsonProperty("token_type")]
    public string TokenType { get; set; }


    //[JsonProperty("id_token")]
    public string IdToken { get; set; }
}
