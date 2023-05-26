using AutoMapper;
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ReadProjectDto>()
        .ReverseMap();

        CreateMap<CandidateProjectDto, CandidateProject>()
        .ForMember(cc => cc.Project, c => c.MapFrom(map => map.Project))
        .ReverseMap();

        CreateMap<CandidateProject, CandidateProjectAddDto>()
        .ReverseMap();

        CreateMap<CandidateProject, CandidateProjectUpdateDto>()
        .ReverseMap();
    }
}
