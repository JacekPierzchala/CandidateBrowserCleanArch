using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateDetailsRequest:IRequest<ServiceReponse<CandidateDetailsDto>>
{
    public int CandidateId { get; set; }
}
public class GetCandidateDetailsRequestHandler : IRequestHandler<GetCandidateDetailsRequest, ServiceReponse<CandidateDetailsDto>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;

    public GetCandidateDetailsRequestHandler(ICandidateRepository candidateRepository,
        IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<CandidateDetailsDto>> Handle(GetCandidateDetailsRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateDetailsDto>();

            var candidate = await _candidateRepository.GetCandidateWithDetailsAsync(request.CandidateId);
            if(candidate == null) 
            {
                throw    new NotFoundException(nameof(Candidate), request.CandidateId);               
            }
            response.Data=_mapper.Map<CandidateDetailsDto>(candidate);
            response.Success = true;

        return response;
    }
}
