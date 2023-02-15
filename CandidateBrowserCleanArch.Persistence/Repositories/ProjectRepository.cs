using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(CandidatesBrowserDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Project>> GetAllActiveProjectAsync()
    {
        throw new NotImplementedException();
    }
}
