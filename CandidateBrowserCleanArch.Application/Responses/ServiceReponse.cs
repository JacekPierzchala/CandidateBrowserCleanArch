﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ServiceReponse<T>:BaseResponse
{
    public T? Data { get; set; }
}
