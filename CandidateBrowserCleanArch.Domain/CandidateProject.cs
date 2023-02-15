namespace CandidateBrowserCleanArch.Domain;

public class CandidateProject:BaseEntity
{
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }

}
