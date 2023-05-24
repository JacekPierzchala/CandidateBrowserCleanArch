using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class CandidateProjectRepository : GenericRepository<CandidateProject>, ICandidateProjectRepository
{
    public CandidateProjectRepository(CandidatesBrowserDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<CandidateProject>> GetAllByCandidateAsync(int candidateId, CancellationToken token)
    {
        var result = await _dbContext.CandidateProjects.Where(c => c.CandidateId == candidateId)
                    .Include(co => co.Project)
                    .ToListAsync();
        return result;
    }

    public async Task<CandidateProject?> GetCandidateProjectDetailsAsync(int id, CancellationToken token)
    {
        return await _dbContext.CandidateProjects.Where(c => c.Id == id)
             .Include(co => co.Project)
             .FirstOrDefaultAsync();
    }
}
