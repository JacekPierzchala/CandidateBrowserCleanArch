using CandidateBrowserCleanArch.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Domain;

public class Company:BaseEntity
{
    [Column(TypeName = "varchar(250)")]
    public string? CompanyName { get; set; }
    public IList<CandidateCompany> Candidates { get; set; } = new List<CandidateCompany>();   
}
