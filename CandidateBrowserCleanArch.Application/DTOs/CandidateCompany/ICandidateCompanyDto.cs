namespace CandidateBrowserCleanArch.Application
{
    public interface ICandidateCompanyDto
    {
        int CandidateId { get; set; }
        int CompanyId { get; set; }
        DateTime? DateEnd { get; set; }
        DateTime DateStart { get; set; }
        string Position { get; set; }
    }
}