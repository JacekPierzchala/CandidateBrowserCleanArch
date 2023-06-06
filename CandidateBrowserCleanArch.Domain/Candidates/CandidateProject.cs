using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateBrowserCleanArch.Domain;

public class CandidateProject : BaseEntity, IAuditableEntity
{
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }


    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "nvarchar(450)")]
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }


    [Column(TypeName = "nvarchar(450)")]
    public string? ModifiedBy { get; set; }
}
