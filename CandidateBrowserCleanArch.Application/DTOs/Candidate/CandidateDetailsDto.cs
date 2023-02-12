using CandidateBrowserCleanArch.Application.DTOs.CandidateCompany;
using CandidateBrowserCleanArch.Application.DTOs.CandidateProject;
using CandidateBrowserCleanArch.Application.DTOs.Common;

namespace CandidateBrowserCleanArch.Application.DTOs.Candidate;

public class CandidateDetailsDto:BaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Desctription { get; set; }
    public string? Email { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<CandidateCompanyDto> Companies { get; set; } = new List<CandidateCompanyDto>();
    public IEnumerable<CandidateProjectDto> Projects { get; set; } = new List<CandidateProjectDto>();
}
