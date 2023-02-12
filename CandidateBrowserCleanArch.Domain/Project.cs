using CandidateBrowserCleanArch.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public class Project:BaseEntity
{
    [Column(TypeName = "varchar(250)")]
    public string? ProjectName { get; set; }
    public IList<CandidateProject> Candidates { get; set; } = new List<CandidateProject>();
}
