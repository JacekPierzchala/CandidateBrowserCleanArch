using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateCompanyDetailsRequest:
    IRequest<ServiceReponse<CandidateCompanyDto>>
{
    public int Id { get; set; }
}
public class GetCandidateCompanyDetailsRequestHandler
    : IRequestHandler<GetCandidateCompanyDetailsRequest, ServiceReponse<CandidateCompanyDto>>
{
    private readonly ICandidateCompanyRepository _candidateCompanyRepository;
    private readonly IMapper _mapper;

    public GetCandidateCompanyDetailsRequestHandler(ICandidateCompanyRepository candidateCompanyRepository,
        IMapper mapper)
    {
        _candidateCompanyRepository = candidateCompanyRepository;
        _mapper = mapper;
    }

    public async Task<ServiceReponse<CandidateCompanyDto>> 
        Handle(GetCandidateCompanyDetailsRequest request, CancellationToken cancellationToken)
    {
        var response=new ServiceReponse<CandidateCompanyDto>();
        var candidateCompany = await _candidateCompanyRepository.GetCandidateCompanyDetailsAsync(request.Id, cancellationToken);
        if (candidateCompany == null)
        {
            throw new NotFoundException(nameof(CandidateCompany), request.Id);
        }
        response.Data = _mapper.Map<CandidateCompanyDto>(candidateCompany);
        response.Success = true;
        return response;
    }
}
