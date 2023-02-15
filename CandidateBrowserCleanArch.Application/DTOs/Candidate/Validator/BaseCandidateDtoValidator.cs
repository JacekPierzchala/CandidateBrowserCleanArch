using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class BaseCandidateDtoValidator : AbstractValidator<ICandidateDto>
{
	public BaseCandidateDtoValidator()
	{
		RuleFor(p=>p.FirstName).NotEmpty().WithMessage("{PropertyName} must be provided");
		RuleFor(p=>p.LastName).NotEmpty().WithMessage("{PropertyName} must be provided");
		RuleFor(p => p.Email)
			.NotEmpty()
			.WithMessage("{PropertyName} must be provided")
			.EmailAddress()
			.WithMessage("Valid email address should be provided");

		RuleFor(p => p.DateOfBirth)
			.NotEmpty()
			.WithMessage("{PropertyName} must be provided");

	}
}

