using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface ICandidateCompanyRepository:IGenericRepository<CandidateCompany>
{
    Task<IEnumerable<CandidateCompany>> GetAllByCandidateAsync(int candidateId,CancellationToken token);
    Task<CandidateCompany?> GetCandidateCompanyDetailsAsync(int  id, CancellationToken token);
}
