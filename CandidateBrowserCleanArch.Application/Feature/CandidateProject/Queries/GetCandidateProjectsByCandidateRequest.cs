using AutoMapper;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateProjectsByCandidateRequest
    : IRequest<ServiceReponse<IEnumerable<CandidateProjectDto>>>
{
    public int CandidateId { get; set; }
}
public class GetCandidateProjectsByCandidateRequestHandler :
    IRequestHandler<GetCandidateProjectsByCandidateRequest, ServiceReponse<IEnumerable<CandidateProjectDto>>>
{
    private readonly ICandidateProjectRepository _candidateProjectRepository;
    private readonly IMapper _mapper;

    public GetCandidateProjectsByCandidateRequestHandler(ICandidateProjectRepository candidateProjectRepository,
        IMapper mapper)
    {
        _candidateProjectRepository = candidateProjectRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<CandidateProjectDto>>> 
        Handle(GetCandidateProjectsByCandidateRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<CandidateProjectDto>>();
        var result = await _candidateProjectRepository.GetAllByCandidateAsync(request.CandidateId, cancellationToken);
        response.Data = _mapper.Map<IEnumerable<CandidateProjectDto>>(result);
        response.Success = true;

        return response;
    }
}
