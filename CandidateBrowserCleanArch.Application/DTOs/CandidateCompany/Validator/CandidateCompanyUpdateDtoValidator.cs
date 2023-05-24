using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class CandidateCompanyUpdateDtoValidator:AbstractValidator<CandidateCompanyUpdateDto>
{
	public CandidateCompanyUpdateDtoValidator(ICandidateRepository candidateRepository,
		ICompanyRepository companyRepository)
	{
		Include(new BaseCandidateCompanyValidator(candidateRepository,companyRepository));
		RuleFor(c=>c.Id).NotEmpty();
	}
}
