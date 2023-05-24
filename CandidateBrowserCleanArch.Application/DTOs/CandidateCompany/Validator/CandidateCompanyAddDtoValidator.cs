using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class CandidateCompanyAddDtoValidator:AbstractValidator<CandidateCompanyAddDto>
{
	public CandidateCompanyAddDtoValidator(ICandidateRepository candidateRepository,
        ICompanyRepository companyRepository)
	{
		Include(new BaseCandidateCompanyValidator(candidateRepository, companyRepository));
	}
}
