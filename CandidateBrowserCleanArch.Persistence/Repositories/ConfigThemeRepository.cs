
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;

namespace CandidateBrowserCleanArch.Persistence;

internal class ConfigThemeRepository : GenericRepository<ConfigTheme>, IConfigThemeRepository
{
    public ConfigThemeRepository(CandidatesBrowserDbContext dbContext) :
        base(dbContext){}

    public async Task<IEnumerable<ConfigTheme>> GetActiveThemesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ConfigThemes.Where(c=>!c.Deleted).ToListAsync();
    }
}
