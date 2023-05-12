using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class BaseIdentityValidator:AbstractValidator<BaseAccountDto>
{
	public BaseIdentityValidator()
	{
		RuleFor(c => c.Email)
			.NotEmpty()
			.NotNull()
            .WithMessage("{PropertyName} cannot be empty");
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("{PropertyName} must have correct email address format");

        RuleFor(c => c.Password)
			.NotEmpty()
			.NotNull()
            .WithMessage("{PropertyName} cannot be empty");

		RuleFor(c => c.Password)
			.MinimumLength(8)
			.WithMessage("{PropertyName} should have at least 8 characters");




    }
}
