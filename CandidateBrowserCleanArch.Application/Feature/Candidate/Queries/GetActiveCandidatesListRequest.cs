using AutoMapper;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetActiveCandidatesListRequest: IRequest<PagedResultResponse<CandidateListDto>>
{
    public CandidateQueryParameters QueryParameters  { get; set; }
}

public class GetActiveCandidatesListRequestHandler : IRequestHandler<GetActiveCandidatesListRequest, PagedResultResponse<CandidateListDto>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;

    public GetActiveCandidatesListRequestHandler(ICandidateRepository candidateRepository, 
        IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }
    public async Task<PagedResultResponse<CandidateListDto>> Handle(GetActiveCandidatesListRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResultResponse<CandidateListDto>();
        //try
        //{
            var result = await _candidateRepository.GetAllActiveCandidatesWithDetailsAsync(request.QueryParameters);
            var candidatesToReturn = _mapper.Map<List<CandidateListDto>>(result.Items);
            
            response.Items = candidatesToReturn;
            response.PageNumber = result.PageNumber;
            response.PageSize = result.PageSize;
            response.TotalCount = result.TotalCount;        
            return response;
            
        //}
        //catch (Exception e)
        //{
        //    throw new Exception(e.Message);
        //}     
   

    }
}

