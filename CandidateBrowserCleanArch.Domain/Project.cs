using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateBrowserCleanArch.Domain;

public class Project:BaseEntity
{
    [Column(TypeName = "varchar(250)")]
    public string? ProjectName { get; set; }
    public IList<CandidateProject> Candidates { get; set; } = new List<CandidateProject>();
}
