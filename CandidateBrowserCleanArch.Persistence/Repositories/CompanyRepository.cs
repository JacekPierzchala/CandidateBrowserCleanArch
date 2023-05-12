using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
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
        var companies = await _dbContext
                        .Companies.Where(c=>!c.Deleted).ToListAsync();
        return companies;
    }
}
