using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity.Mappers
{
    internal class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,User>()
                .ReverseMap();
            CreateMap<IdentityRole, Role>().ReverseMap();
        }
    }
}
