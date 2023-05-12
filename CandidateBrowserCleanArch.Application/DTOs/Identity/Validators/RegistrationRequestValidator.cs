using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class RegistrationRequestValidator:AbstractValidator<RegistrationRequest>
{
	public RegistrationRequestValidator()
	{
		Include(new BaseIdentityValidator());

		RuleFor(c=>c.FirstName).NotEmpty().NotNull()
			.WithMessage("{PropertyName} cannot be empty");

        RuleFor(c => c.FirstName).MinimumLength(5)
        .WithMessage("{PropertyName} should have at least 5 characters");

        RuleFor(c => c.FirstName)
        .MaximumLength(20)
		.WithMessage("{PropertyName} should have maximum 20 characters");

        RuleFor(c => c.LastName)
            .NotEmpty().NotNull()
        .WithMessage("{PropertyName} cannot be empty");

        RuleFor(c => c.LastName)
            .MinimumLength(5)
        .WithMessage("{PropertyName} should have at least 5 characters");

        RuleFor(c => c.LastName)
            .MaximumLength(20)
        .WithMessage("{PropertyName} should have maximum 20 characters");

        RuleFor(c=>c.ConfirmPassword).NotEmpty().NotNull()
            .WithMessage("{PropertyName} cannot be empty");
        
		RuleFor(c => c.ConfirmPassword).Equal(c=>c.Password)
		.WithMessage("Password and confirm Password must match");

        RuleFor(c => c.ReturnUrl).NotEmpty().NotNull()
        .WithMessage("{PropertyName} cannot be empty");
    }
}
