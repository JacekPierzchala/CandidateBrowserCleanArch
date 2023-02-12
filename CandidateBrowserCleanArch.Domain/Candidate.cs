using CandidateBrowserCleanArch.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public class Candidate:BaseEntity
{
    [Column(TypeName = "nvarchar(150)")]
    public string? FirstName { get; set; }
    [Column(TypeName = "nvarchar(150)")]
    public string? LastName { get; set; }


    public DateTime DateOfBirth { get; set; }

    [Column(TypeName = "nvarchar(MAX)")]
    public string? Desctription { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Email { get; set; }

    [Column(TypeName = "varchar(MAX)")]
    public string? ProfilePicture { get; set; }

    public IList<CandidateCompany> Companies { get; set; } = new List<CandidateCompany>();
    public IList<CandidateProject> Projects { get; set; } = new List<CandidateProject>();
}
