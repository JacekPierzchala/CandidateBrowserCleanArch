using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetRolesRequest:IRequest<ServiceReponse<IEnumerable<RoleDto>>>
{
}

public class GetRolesRequestHandler :
    IRequestHandler<GetRolesRequest, ServiceReponse<IEnumerable<RoleDto>>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public GetRolesRequestHandler(IRoleRepository roleRepository, 
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<RoleDto>>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<RoleDto>>();
        var dbData = await _roleRepository.GetRolesAsync();
        response.Data = _mapper.Map<IEnumerable<RoleDto>>(dbData);
        response.Success = true;
        return response;
    }
}