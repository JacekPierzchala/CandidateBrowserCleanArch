using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;

namespace CandidateBrowserCleanArch.Persistence;

internal class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
{
    public UserSettingsRepository(CandidatesBrowserDbContext dbContext) : 
        base(dbContext){}

    public async Task<UserSettings?> GetUserSettingsAsync(string userId)
    {
        return await _dbContext.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId);  
    }
}
