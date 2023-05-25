namespace CandidateBrowserCleanArch.Application;

public class CandidateProjectDto:BaseDto
{
    public ReadProjectDto Project { get; set; }
    public int CandidateId { get; set; }
    public int Id { get; set; }
}
