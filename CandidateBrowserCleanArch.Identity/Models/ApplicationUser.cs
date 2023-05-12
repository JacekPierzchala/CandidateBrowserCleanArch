using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Identity;

public class ApplicationUser:IdentityUser
{
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    public DateTime DateLogged { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
