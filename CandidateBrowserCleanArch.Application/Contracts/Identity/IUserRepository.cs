using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface IUserRepository
{
    Task<IEnumerable<User>>GetUsersAsync();
    Task<User>UpdateUser(User user);
    Task<User>GetUser(string userId);
}
