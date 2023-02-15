namespace CandidateBrowserCleanArch.Domain;

public class CandidateCompany : BaseEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }


    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }
}
