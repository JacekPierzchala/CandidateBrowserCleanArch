﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AuthResponse:BaseResponse
{
    public string? Token { get; set; }
    public string RefreshToken { get; set; }
}
