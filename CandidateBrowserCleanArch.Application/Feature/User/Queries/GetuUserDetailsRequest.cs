using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetUserDetailsRequest:IRequest<ServiceReponse<ReadUserDetailsDto>>
{
    public string UserId { get; set; }
}

public class GetUserDetailsRequestHandler : IRequestHandler<GetUserDetailsRequest, ServiceReponse<ReadUserDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserDetailsRequestHandler
        (IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<ServiceReponse<ReadUserDetailsDto>> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
    {
        var response=new ServiceReponse<ReadUserDetailsDto>();
        var userDb=await _userRepository.GetUserWithDetails(request.UserId);
        if (userDb==null) 
        { 
            throw new NotFoundException(nameof(User),request.UserId);   
        }
        response.Data=_mapper.Map<ReadUserDetailsDto>(userDb);
        response.Success=true;
        return response;
    }
}