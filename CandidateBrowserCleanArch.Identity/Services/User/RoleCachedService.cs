using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateBrowserCleanArch.Identity;

internal class RoleCachedService : IRoleRepository
{
    private readonly IRoleRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public RoleCachedService(IRoleRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }
    public async Task<IEnumerable<Role>> GetRolesAsync()
    {

        string key = "rolesList";
        return await _memoryCache.GetOrCreateAsync(
            key,
            async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return await _decorated.GetRolesAsync();
            });       
    }
}
