using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class CandidateProjectUpdateDtoValidator:AbstractValidator<CandidateProjectUpdateDto>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IProjectRepository _projectRepository;

    public CandidateProjectUpdateDtoValidator(ICandidateRepository candidateRepository,
        IProjectRepository projectRepository)
	{
        _candidateRepository = candidateRepository;
        _projectRepository = projectRepository;
        Include(new BaseCandidateProjectValidator(_candidateRepository,_projectRepository));
        RuleFor(c => c.Id).NotEmpty().NotNull();

    }
}
