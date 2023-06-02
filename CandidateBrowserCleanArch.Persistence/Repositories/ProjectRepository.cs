using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(CandidatesBrowserDbContext dbContext) : base(dbContext)
    {}

    public async Task<IEnumerable<Project>> GetAllActiveProjectsAsync()
                => await _dbContext
                        .Projects.Where(c => !c.Deleted).ToListAsync();

}
