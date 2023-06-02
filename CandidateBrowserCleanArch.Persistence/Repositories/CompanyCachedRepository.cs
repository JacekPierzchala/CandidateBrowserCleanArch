using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateBrowserCleanArch.Persistence;

public class CompanyCachedRepository : GenericRepository<Company>, ICompanyRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICompanyRepository _decorated;

    public CompanyCachedRepository(IMemoryCache memoryCache,
        ICompanyRepository decorated,
        CandidatesBrowserDbContext dbContext) : base(dbContext)
    {
        _memoryCache = memoryCache;
        _decorated = decorated;
    }
  

    public async Task<IEnumerable<Company>> GetAllActiveCompaniesAsync()
    {
        string key = "companiesList";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return await _decorated.GetAllActiveCompaniesAsync();
            });
    }


}
