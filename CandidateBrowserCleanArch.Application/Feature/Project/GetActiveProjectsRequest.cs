using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetActiveProjectsRequest:IRequest<ServiceReponse<IEnumerable<ReadProjectDto>>>
{

}
public class GetActiveProjectsRequestHandler : 
    IRequestHandler<GetActiveProjectsRequest, ServiceReponse<IEnumerable<ReadProjectDto>>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public GetActiveProjectsRequestHandler(IProjectRepository projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<ReadProjectDto>>> Handle(GetActiveProjectsRequest request, CancellationToken cancellationToken)
    {
        var response =new  ServiceReponse<IEnumerable<ReadProjectDto>>();
        var result = await _projectRepository.GetAllActiveProjectsAsync();
        response.Data=_mapper.Map<IEnumerable<ReadProjectDto>>(result);

        return response;
    }
}
