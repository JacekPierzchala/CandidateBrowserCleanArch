using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetActiveCompaniesRequest:IRequest<ServiceReponse<IEnumerable<ReadCompanyDto>>>
{

}

public class GetActiveCompaniesRequestHandler : 
    IRequestHandler<GetActiveCompaniesRequest, ServiceReponse<IEnumerable<ReadCompanyDto>>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetActiveCompaniesRequestHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<IEnumerable<ReadCompanyDto>>> Handle(GetActiveCompaniesRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<ReadCompanyDto>>();
        var result = await _companyRepository.GetAllActiveCompaniesAsync();
        response.Data=_mapper.Map<IEnumerable<ReadCompanyDto>>(result);
        response.Success = true;
        return response;
    }
}