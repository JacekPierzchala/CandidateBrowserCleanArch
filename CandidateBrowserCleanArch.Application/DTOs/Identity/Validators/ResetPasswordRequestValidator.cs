using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ResetPasswordRequestValidator:AbstractValidator<ResetPasswordRequest>
{
	public ResetPasswordRequestValidator()
	{
        RuleFor(c => c.NewPassword).NotEmpty().NotNull();
        RuleFor(c => c.ConfirmNewPassword).NotEmpty().NotNull();
        RuleFor(c => c.Token).NotEmpty().NotNull();
        RuleFor(c => c.ConfirmNewPassword).Equal(c => c.NewPassword)
        .WithMessage("Password and Confirm Password must match");
    }
}
