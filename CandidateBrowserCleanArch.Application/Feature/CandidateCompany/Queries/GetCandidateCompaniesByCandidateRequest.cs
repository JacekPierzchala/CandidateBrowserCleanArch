using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateCompaniesByCandidateRequest:
    IRequest<ServiceReponse<IEnumerable<CandidateCompanyDto>>>
{
    public int CandidateId { get; set; }
}
public class GetCandidateCompaniesByCandidateRequestHandler :
        IRequestHandler<GetCandidateCompaniesByCandidateRequest, ServiceReponse<IEnumerable<CandidateCompanyDto>>>
{
    private readonly ICandidateCompanyRepository _candidateCompanyRepository;
    private readonly IMapper _mapper;

    public GetCandidateCompaniesByCandidateRequestHandler(ICandidateCompanyRepository candidateCompanyRepository,
        IMapper mapper)
    {
        _candidateCompanyRepository = candidateCompanyRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<CandidateCompanyDto>>> 
        Handle(GetCandidateCompaniesByCandidateRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<CandidateCompanyDto>>();
        var result = await _candidateCompanyRepository.GetAllByCandidateAsync(request.CandidateId, cancellationToken);
        response.Data = _mapper.Map<IEnumerable<CandidateCompanyDto>>(result);
        response.Success = true;

        return response;
    }
}