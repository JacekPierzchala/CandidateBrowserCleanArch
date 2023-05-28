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

internal class UserService : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<User?> GetUserWithDetails(string userId)
    {
        User? user=null;
        var roles=await _roleManager.Roles.ToListAsync();
        var userDb=await _userManager.FindByIdAsync(userId);
        if (userDb!=null) 
        {
            var roleNames = await _userManager.GetRolesAsync(userDb);
            user = _mapper.Map<User>(userDb);
            user.RoleNames = string.Join(", ", roleNames);
            user.Roles = _mapper.Map<ICollection<Role>>(roles.Where(c => roleNames.Contains(c.Name)).ToList());
            
        }
        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var usersDb = await _userManager.Users.ToListAsync();
        var users = _mapper.Map<IEnumerable<User>>(usersDb);
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(usersDb.FirstOrDefault(c => c.Id == user.Id));
            user.RoleNames=string.Join(", ", roles); 
        }
        return users;
    }

    public async Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUser(string userId)
    {
        User? user = null;
        var userDb = await _userManager.FindByIdAsync(userId);
        if (userDb != null)
        {
            user = _mapper.Map<User>(userDb);
        }
        return user;
    }
}
