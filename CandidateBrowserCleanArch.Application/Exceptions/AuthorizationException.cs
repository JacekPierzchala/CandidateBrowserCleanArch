﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AuthorizationException:ApplicationException
{
	public AuthorizationException(string message):base(message)
	{

	}
}
