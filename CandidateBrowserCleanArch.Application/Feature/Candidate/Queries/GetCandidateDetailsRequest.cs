using AutoMapper;
using CandidateBrowserCleanArch.Application.Contracts.Persistence;
using CandidateBrowserCleanArch.Application.DTOs.Candidate;
using CandidateBrowserCleanArch.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Feature.Candidate.Queries;

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
        try
        {
            var candidate = await _candidateRepository.GetCandidateWithDetailsAsync(request.CandidateId);
            response.Data=_mapper.Map<CandidateDetailsDto>(candidate);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message=ex.Message;
        }

        return response;
    }
}
