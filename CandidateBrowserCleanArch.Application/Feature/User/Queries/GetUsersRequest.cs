using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetUsersRequest:IRequest<ServiceReponse<IEnumerable<ReadUserListDto>>>
{
}

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, ServiceReponse<IEnumerable<ReadUserListDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersRequestHandler(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<ReadUserListDto>>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<ReadUserListDto>>();
        var dbData = await _userRepository.GetUsersAsync();
        response.Data=_mapper.Map<IEnumerable<ReadUserListDto>>(dbData); 
        response.Success= true;

        return response;
        

    }
}