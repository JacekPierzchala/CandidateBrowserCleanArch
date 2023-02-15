namespace CandidateBrowserCleanArch.Application;

public class CandidateListDto:BaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IEnumerable<CandidateCompanyDto> Companies { get; set; }= new List<CandidateCompanyDto>();
    public IEnumerable<CandidateProjectDto> Projects { get; set; }= new List<CandidateProjectDto>();

}
