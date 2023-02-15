using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateBrowserCleanArch.Domain;

public class Company:BaseEntity
{
    [Column(TypeName = "varchar(250)")]
    public string? CompanyName { get; set; }
    public IList<CandidateCompany> Candidates { get; set; } = new List<CandidateCompany>();   
}
