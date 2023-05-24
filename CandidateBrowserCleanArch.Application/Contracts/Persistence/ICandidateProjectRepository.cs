using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface ICandidateProjectRepository:IGenericRepository<CandidateProject>
{
    Task<IEnumerable<CandidateProject>> GetAllByCandidateAsync(int candidateId, CancellationToken token);
    Task<CandidateProject?> GetCandidateProjectDetailsAsync(int id, CancellationToken token);
}
