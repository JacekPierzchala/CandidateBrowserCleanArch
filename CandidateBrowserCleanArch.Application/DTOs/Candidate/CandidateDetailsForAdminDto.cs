namespace CandidateBrowserCleanArch.Application;

public class CandidateDetailsForAdminDto: BaseDto
{
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public ReadUserDetailsDto? CreatedByUser { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }
    public ReadUserDetailsDto? ModifiedByUser { get; set; }
}