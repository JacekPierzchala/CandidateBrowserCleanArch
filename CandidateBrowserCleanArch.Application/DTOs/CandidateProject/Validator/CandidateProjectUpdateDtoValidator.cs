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
    private readonly ICandidateProjectRepository _candidateProjectRepository;

    public CandidateProjectUpdateDtoValidator(ICandidateRepository candidateRepository,
        IProjectRepository projectRepository, ICandidateProjectRepository candidateProjectRepository)
	{
        _candidateRepository = candidateRepository;
        _projectRepository = projectRepository;
        _candidateProjectRepository = candidateProjectRepository;
        Include(new BaseCandidateProjectValidator(_candidateRepository,_projectRepository));
        RuleFor(c => c.Id).NotEmpty().NotNull();

        RuleFor(c => c.ProjectId).MustAsync(async (candidateP, id, token) =>
        {
            var result = await _candidateProjectRepository.GetAllByCandidateAsync(candidateP.CandidateId, token);
            return !result.Any(c => c.ProjectId == id  && c.Id!=candidateP.Id);
        }).WithMessage("{PropertyName} already assigned to this candidate"); ;

    }
}
