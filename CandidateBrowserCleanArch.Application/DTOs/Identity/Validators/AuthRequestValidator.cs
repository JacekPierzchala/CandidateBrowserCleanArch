using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AuthRequestValidator:AbstractValidator<AuthRequest>
{
	public AuthRequestValidator()
	{
		Include(new BaseIdentityValidator());
	}
}
