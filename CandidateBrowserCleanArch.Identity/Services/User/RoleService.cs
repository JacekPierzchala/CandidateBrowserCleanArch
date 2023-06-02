using AutoMapper;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity;

internal class RoleService : IRoleRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public RoleService(RoleManager<IdentityRole> roleManager, 
        IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return  _mapper.Map<ICollection<Role>>(roles);
    }
}
