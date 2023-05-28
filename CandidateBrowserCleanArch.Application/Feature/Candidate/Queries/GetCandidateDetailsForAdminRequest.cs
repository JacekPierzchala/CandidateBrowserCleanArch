using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetCandidateDetailsForAdminRequest: 
    IRequest<ServiceReponse<CandidateDetailsForAdminDto>>
{
    public int CandidateId { get; set; }
}
public class GetCandidateDetailsForAdminRequestHandler :
    IRequestHandler<GetCandidateDetailsForAdminRequest, ServiceReponse<CandidateDetailsForAdminDto>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetCandidateDetailsForAdminRequestHandler(ICandidateRepository candidateRepository,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<ServiceReponse<CandidateDetailsForAdminDto>> 
        Handle(GetCandidateDetailsForAdminRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateDetailsForAdminDto>();
        var candidate = await _candidateRepository.GetAsync(request.CandidateId);
        if (candidate == null)
        {
            throw new NotFoundException(nameof(Candidate), request.CandidateId);
        }
        var candidateToReturn = _mapper.Map<CandidateDetailsForAdminDto>(candidate);

        var createdUser = await _userRepository.GetUser(candidate.CreatedBy);
        if(createdUser!=null)
        {
            candidateToReturn.CreatedByUser= _mapper.Map<ReadUserDetailsDto>(createdUser);
        }
        var modifiedUser = await _userRepository.GetUser(candidate.ModifiedBy);
        if (modifiedUser != null)
        {
            candidateToReturn.ModifiedByUser =_mapper.Map<ReadUserDetailsDto>(modifiedUser);
        }
        response.Data= candidateToReturn;    
        response.Success = true;
        return response;



    }
}