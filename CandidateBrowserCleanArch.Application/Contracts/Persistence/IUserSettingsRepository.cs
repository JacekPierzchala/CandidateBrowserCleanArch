using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface IUserSettingsRepository:IGenericRepository<UserSettings>
{
    Task<UserSettings?> GetUserSettingsAsync(string userId);
}
