using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public class User
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public string RoleNames { get; set; }
    public DateTime DateRegistered { get; set; }
    public DateTime? DateLogged { get; set; }
}
