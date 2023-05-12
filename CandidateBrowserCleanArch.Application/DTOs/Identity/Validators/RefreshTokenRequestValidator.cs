using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class RefreshTokenRequestValidator:AbstractValidator<RefreshTokenRequest>    
{
	public RefreshTokenRequestValidator()
	{
		RuleFor(c=>c.RefreshToken).NotEmpty()
			.NotNull()
			.WithMessage("{PropertyName} cannot be null");

        RuleFor(c => c.Token).NotEmpty()
			.NotNull()
			.WithMessage("{PropertyName} cannot be null");
    }
}
