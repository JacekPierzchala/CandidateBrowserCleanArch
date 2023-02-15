using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface ICandidateRepository: IGenericRepository<Candidate>
{
    Task<PagedResultResponse<Candidate>> GetAllActiveCandidatesWithDetailsAsync(CandidateQueryParameters QueryParameters);
    Task<Candidate> GetCandidateWithDetailsAsync(int id);
}
