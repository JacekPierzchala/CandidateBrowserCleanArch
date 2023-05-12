using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class ConfirmEmailRequestValidator: AbstractValidator<ConfirmEmailRequest>
{
	public ConfirmEmailRequestValidator()
	{
		RuleFor(p=>p.UserId).NotEmpty().NotNull();
		RuleFor(p=>p.Token).NotEmpty().NotNull();
	}
}
