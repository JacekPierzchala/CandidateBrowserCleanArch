
using System.ComponentModel.DataAnnotations.Schema;

namespace CandidateBrowserCleanArch.Application;

public class CandidateDetailsDto:BaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? ProfilePicture { get; set; }
    public string? ProfilePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<CandidateCompanyDto> Companies { get; set; } = new List<CandidateCompanyDto>();
    public IEnumerable<CandidateProjectDto> Projects { get; set; } = new List<CandidateProjectDto>();
}
