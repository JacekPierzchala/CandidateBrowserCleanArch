using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ReadUserDetailsDto:ReadUserListDto
{
    public DateTime DateRegistered { get; set; }
    public DateTime DateLogged { get; set; }
    public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
}
