namespace CandidateBrowserCleanArch.Application;

public class CandidateCompanyDto:BaseDto
{
    public CompanyDto Company { get; set; }
    public DateTime DateStart { get; set; }
    public string Position { get; set; }
    public DateTime? DateEnd { get; set; }
}
