using FluentValidation;

namespace CandidateBrowserCleanArch.Application;

public class ForgotPasswordRequestValidator:AbstractValidator<ForgotPasswordRequest>
{
	public ForgotPasswordRequestValidator()
	{
        RuleFor(c => c.Email).NotEmpty().NotNull();

        RuleFor(c => c.Email)
        .EmailAddress()
        .WithMessage("{PropertyName} must have correct email address format");
        RuleFor(c => c.ReturnUrl).NotEmpty().NotNull();
    }
}
