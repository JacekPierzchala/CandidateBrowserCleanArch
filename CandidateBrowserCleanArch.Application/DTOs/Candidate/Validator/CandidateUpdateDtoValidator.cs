using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class CandidateUpdateDtoValidator: AbstractValidator<CandidateUpdateDto>
{
	public CandidateUpdateDtoValidator()
	{
        RuleFor(p=>p.Id).NotEmpty()
            .WithMessage("{PropertyName} Must Be Present");
        Include(new BaseCandidateDtoValidator());
    }
}
