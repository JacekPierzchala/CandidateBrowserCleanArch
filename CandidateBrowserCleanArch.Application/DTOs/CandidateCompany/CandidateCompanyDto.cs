namespace CandidateBrowserCleanArch.Application;

public class CandidateCompanyDto:BaseDto
{
    public int Id { get; set; }
    public ReadCompanyDto Company { get; set; }
    public int CandidateId { get; set; }
    public DateTime DateStart { get; set; }
    public string Position { get; set; }
    public DateTime? DateEnd { get; set; }
}
