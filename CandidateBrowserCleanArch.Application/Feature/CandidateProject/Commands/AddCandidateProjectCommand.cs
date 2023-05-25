using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AddCandidateProjectCommand: IRequest<ServiceReponse<CandidateProjectDto>>
{
    public CandidateProjectAddDto CandidateProjectAddDto { get; set; }
}
public class AddCandidateProjectCommandHandler :
    IRequestHandler<AddCandidateProjectCommand, ServiceReponse<CandidateProjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddCandidateProjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceReponse<CandidateProjectDto>> Handle(AddCandidateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateProjectDto>();
        var validator = new CandidateProjectAddDtoValidator(_unitOfWork.CandidateRepository, _unitOfWork.ProjectRepository,_unitOfWork.CandidateProjectRepository);
        var validationResult = await validator.ValidateAsync(request.CandidateProjectAddDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var candidateProject = _mapper.Map<CandidateProject>(request.CandidateProjectAddDto);
        await _unitOfWork.CandidateProjectRepository.AddAsync(candidateProject);
        response.Success = await _unitOfWork.SaveAsync();
        if (response.Success)
        {
            response.Data = _mapper.Map<CandidateProjectDto>
                (await _unitOfWork.CandidateProjectRepository.GetCandidateProjectDetailsAsync(candidateProject.Id, cancellationToken));
        }
        else
        {
            throw new Exception("Error occurred during this operation");
        }

        return response;
    }
}