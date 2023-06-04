using AutoMapper;
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUserListDto>()
        .ReverseMap();
        CreateMap<User, ReadUserDetailsDto>()
        .ReverseMap();
        CreateMap<User, UpdateUserDto>()
        .ReverseMap();
        CreateMap<Role, RoleDto>()
        .ReverseMap();
    }
}