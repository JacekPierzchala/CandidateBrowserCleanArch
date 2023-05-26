using AutoMapper;
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public class CandidateProfile : Profile
{
    public CandidateProfile()
    {

        CreateMap<Candidate, CandidateListDto>()
            .ForMember(cc => cc.Projects, c => c.MapFrom(map => map.Projects.Take(3)))
            .ForMember(cc => cc.Companies, c => c.MapFrom(map => map.Companies.OrderByDescending(c => c.DateStart).Take(3)))
            .ReverseMap();

        CreateMap<Candidate, CandidateDetailsDto>()
            .ReverseMap();
        CreateMap<Candidate, CandidateCreateDto>()
            .ReverseMap();
        CreateMap<Candidate, CandidateUpdateDto>()
            .ReverseMap();
    }
}
