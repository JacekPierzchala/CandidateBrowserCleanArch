using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class CandidateProjectAddDtoValidator:
    AbstractValidator<CandidateProjectAddDto>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IProjectRepository _projectRepository;

    public CandidateProjectAddDtoValidator(ICandidateRepository candidateRepository,
		IProjectRepository projectRepository)
	{
        _candidateRepository = candidateRepository;
        _projectRepository = projectRepository;
        Include(new BaseCandidateProjectValidator(_candidateRepository, _projectRepository));

    }
}
