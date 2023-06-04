using AutoMapper;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.AspNetCore.Components.Forms;
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
        User? user = null;
        var roles = await _roleManager.Roles.ToListAsync();
        var userDb = await _userManager.FindByIdAsync(userId);
        if (userDb != null)
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
            user.RoleNames = string.Join(", ", roles);
        }
        return users;
    }

    public async Task<(bool result, string message)> UpdateUser(User user)
    {
        var existingUser = await _userManager.FindByIdAsync(user.Id);
        if (existingUser == null)
        {
            return (false, "user not found");
        }
        try
        {
            _mapper.Map(user, existingUser);
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            var rolesAll = await _roleManager.Roles.ToListAsync();
            await _userManager.RemoveFromRolesAsync(existingUser, userRoles);
            await _userManager.AddToRolesAsync(existingUser, rolesAll.Where(c => user.Roles.Any(r => r.Id == c.Id))
                .Select(r=>r.Name)
                .ToList());
            await _userManager.UpdateAsync(existingUser);
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }


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
