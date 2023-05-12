using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class ConfirmEmailRepeatRequestValidator:AbstractValidator<ConfirmEmailRepeatRequest>
{
	public ConfirmEmailRepeatRequestValidator()
	{
		RuleFor(c => c.Email).NotEmpty().NotNull();
		RuleFor(c => c.ReturnUrl).NotEmpty().NotNull();

	}
}
