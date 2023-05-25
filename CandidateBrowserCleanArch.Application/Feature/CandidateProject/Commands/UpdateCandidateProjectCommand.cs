using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class UpdateCandidateProjectCommand: IRequest<ServiceReponse<CandidateProjectDto>>
{
    public int Id { get; set; }
    public CandidateProjectUpdateDto CandidateProjectUpdateDto { get; set; }
}
public class UpdateCandidateProjectCommandHandler :
    IRequestHandler<UpdateCandidateProjectCommand, ServiceReponse<CandidateProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCandidateProjectCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<CandidateProjectDto>> Handle(UpdateCandidateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateProjectDto>();

        if (request.Id != request.CandidateProjectUpdateDto.Id || request.Id==0 || request.CandidateProjectUpdateDto.Id==0)
        {
            throw new BadRequestException("Inconsistent Id");
        }
        var validator = new CandidateProjectUpdateDtoValidator(_unitOfWork.CandidateRepository, 
            _unitOfWork.ProjectRepository, _unitOfWork.CandidateProjectRepository);

        var validationResult = await validator.ValidateAsync(request.CandidateProjectUpdateDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var candidateProject = await _unitOfWork.CandidateProjectRepository.GetAsync(request.Id);
        if (candidateProject == null)
        {
            throw new NotFoundException(nameof(CandidateProject), request.Id);
        }

        _mapper.Map(request.CandidateProjectUpdateDto, candidateProject);
        await _unitOfWork.CandidateProjectRepository.UpdateAsync(candidateProject);

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