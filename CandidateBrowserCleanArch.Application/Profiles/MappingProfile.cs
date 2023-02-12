using AutoMapper;
using CandidateBrowserCleanArch.Application.DTOs.Candidate;
using CandidateBrowserCleanArch.Application.DTOs.CandidateCompany;
using CandidateBrowserCleanArch.Application.DTOs.CandidateProject;
using CandidateBrowserCleanArch.Application.DTOs.Company;
using CandidateBrowserCleanArch.Application.DTOs.Project;
using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Profiles;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ReverseMap();
        CreateMap<Project, ProjectDto>()
            .ReverseMap();

        CreateMap<CandidateCompany, CandidateCompanyDto>()
            .ForMember(cc => cc.Company, c => c.MapFrom(map => map.Company))
        .ReverseMap();

        CreateMap<CandidateProjectDto, CandidateProject>()
        .ForMember(cc => cc.Project, c => c.MapFrom(map => map.Project))
        .ReverseMap();

        CreateMap<Candidate, CandidateListDto>()
            .ForMember(cc => cc.Projects, c => c.MapFrom(map => map.Projects.Take(3)))
            .ForMember(cc => cc.Companies, c => c.MapFrom(map => map.Companies.OrderByDescending(c=>c.DateStart).Take(3)))
            .ReverseMap();


        CreateMap<Candidate, CandidateDetailsDto>()
            .ReverseMap();
    }
}
