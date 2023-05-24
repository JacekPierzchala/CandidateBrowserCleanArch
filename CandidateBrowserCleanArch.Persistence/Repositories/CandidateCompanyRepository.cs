using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class CandidateCompanyRepository : GenericRepository<CandidateCompany>, ICandidateCompanyRepository
{
    public CandidateCompanyRepository(CandidatesBrowserDbContext dbContext) : base(dbContext){}

    public async Task<CandidateCompany?> GetCandidateCompanyDetailsAsync(int id, CancellationToken token)
    {
       return await _dbContext.CandidateCompanies.Where(c=>c.Id==id)
            .Include(co => co.Company)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CandidateCompany>> GetAllByCandidateAsync(int candidateId, CancellationToken token)
    {
        var result=await  _dbContext.CandidateCompanies.Where(c => c.CandidateId == candidateId)
            .Include(co=>co.Company)
            .ToListAsync();
        return result;
    }
}
