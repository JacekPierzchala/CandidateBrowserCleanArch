using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateBrowserCleanArch.Persistence;

public class ProjectCachedRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly IProjectRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public ProjectCachedRepository(CandidatesBrowserDbContext dbContext, 
        IProjectRepository decorated,
        IMemoryCache memoryCache) : 
        base(dbContext)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Project>> GetAllActiveProjectsAsync()
    {
        string key = "projectList";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return await _decorated.GetAllActiveProjectsAsync();
            });
    }
}