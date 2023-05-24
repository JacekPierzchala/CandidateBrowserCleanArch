using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface ICompanyRepository:IGenericRepository<Company>
{
    Task<IEnumerable<Company>> GetAllActiveCompaniesAsync();
}
