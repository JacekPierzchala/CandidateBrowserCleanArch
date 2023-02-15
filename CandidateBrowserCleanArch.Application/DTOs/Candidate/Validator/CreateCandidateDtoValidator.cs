using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class CreateCandidateDtoValidator:AbstractValidator<CreateCandidateDto>
{
	public CreateCandidateDtoValidator()
	{
		Include(new BaseCandidateDtoValidator());
	}
}
