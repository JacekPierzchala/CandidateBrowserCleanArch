using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(CandidatesBrowserDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Company>> GetAllActiveCompaniesAsync()
    {
        throw new NotImplementedException();
    }
}
