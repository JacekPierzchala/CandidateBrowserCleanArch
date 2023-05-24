using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

internal class BaseCandidateProjectValidator:AbstractValidator<ICandidateProjectDto>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IProjectRepository _projectRepository;

    public BaseCandidateProjectValidator(ICandidateRepository candidateRepository,
		IProjectRepository projectRepository)
	{
        _candidateRepository = candidateRepository;
        _projectRepository = projectRepository;
        RuleFor(c => c.CandidateId).NotEmpty().NotNull();
        RuleFor(c => c.ProjectId).NotEmpty().NotNull();
        RuleFor(c => c.ProjectId).MustAsync(async (id, token) =>
        {
            return await _projectRepository.Exists(id);
        }).WithMessage("{PropertyName} does not exist");
        RuleFor(c => c.CandidateId).MustAsync(async (id, token) =>
        {
            return await _candidateRepository.Exists(id);
        }).WithMessage("{PropertyName} does not exist");
    }
}
