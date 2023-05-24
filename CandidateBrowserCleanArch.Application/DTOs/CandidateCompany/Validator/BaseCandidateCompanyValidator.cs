using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class BaseCandidateCompanyValidator:AbstractValidator<ICandidateCompanyDto>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICompanyRepository _companyRepository;

    public BaseCandidateCompanyValidator(ICandidateRepository candidateRepository, 
		ICompanyRepository companyRepository)
	{
        _candidateRepository = candidateRepository;
        _companyRepository = companyRepository;
        RuleFor(c=>c.CandidateId).NotEmpty().NotNull();
		RuleFor(c=>c.CompanyId).NotEmpty().NotNull();
		RuleFor(c=>c.Position).NotEmpty().NotNull(); ;
		RuleFor(c=>c.DateStart).NotEmpty().NotNull(); 
		RuleFor(c => c.DateStart.Date).LessThanOrEqualTo(c => c.DateEnd.Value.Date);
        RuleFor(c => c.DateEnd.Value.Date).GreaterThanOrEqualTo(c => c.DateStart.Date);

        RuleFor(c => c.CandidateId).MustAsync(async (id,token) => 
        {
            return  await _candidateRepository.Exists(id);          
        }).WithMessage("{PropertyName} does not exist"); 
        RuleFor(c => c.CompanyId).MustAsync(async (id, token) =>
        {
            return await _companyRepository.Exists(id);
        }).WithMessage("{PropertyName} does not exist"); ;

    }
}
