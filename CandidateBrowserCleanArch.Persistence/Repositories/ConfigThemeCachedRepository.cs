using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateBrowserCleanArch.Persistence;

internal class ConfigThemeCachedRepository : GenericRepository<ConfigTheme>, IConfigThemeRepository
{
    private readonly IConfigThemeRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public ConfigThemeCachedRepository(CandidatesBrowserDbContext dbContext,
        IConfigThemeRepository decorated,
        IMemoryCache memoryCache) 
        : base(dbContext)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<ConfigTheme>> GetActiveThemesAsync(CancellationToken cancellationToken)
    {
        string key = "themesList";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return await _decorated.GetActiveThemesAsync(cancellationToken);
            });
    }
}
