using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateBrowserCleanArch.Domain;

public class CandidateCompany : BaseEntity, IAuditableEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }

    [Column(TypeName = "nvarchar(250)")]
    public string? Position { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }


    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "nvarchar(450)")]
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }


    [Column(TypeName = "nvarchar(450)")]
    public string? ModifiedBy { get; set; }
}
