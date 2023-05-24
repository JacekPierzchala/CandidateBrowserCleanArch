using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateProjectsDetailsRequest
    : IRequest<ServiceReponse<CandidateProjectDto>>
{
    public int Id { get; set; }
}
public class GetCandidateProjectsDetailsRequestHandler :
    IRequestHandler<GetCandidateProjectsDetailsRequest, ServiceReponse<CandidateProjectDto>>
{
    private readonly ICandidateProjectRepository _candidateProjectRepository;
    private readonly IMapper _mapper;

    public GetCandidateProjectsDetailsRequestHandler(ICandidateProjectRepository candidateProjectRepository,
        IMapper mapper)
    {
        _candidateProjectRepository = candidateProjectRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<CandidateProjectDto>> 
        Handle(GetCandidateProjectsDetailsRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateProjectDto>();
        var candidateProject = await _candidateProjectRepository.GetCandidateProjectDetailsAsync(request.Id, cancellationToken);
        if (candidateProject == null)
        {
            throw new NotFoundException(nameof(CandidateProject), request.Id);
        }
        response.Data = _mapper.Map<CandidateProjectDto>(candidateProject);
        response.Success = true;
        return response;
    }
}