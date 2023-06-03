using AutoMapper;
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, ReadCompanyDto>()
        .ReverseMap();


        CreateMap<CandidateCompany, CandidateCompanyDto>()
        .ForMember(cc => cc.Company, c => c.MapFrom(map => map.Company))
        .ReverseMap();

        CreateMap<CandidateCompany, CandidateCompanyAddDto>()
        .ReverseMap();

        CreateMap<CandidateCompany, CandidateCompanyUpdateDto>()
        .ReverseMap();
    }
}
