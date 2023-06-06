using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface IConfigThemeRepository : IGenericRepository<ConfigTheme>
{
    Task<IEnumerable<ConfigTheme>> GetActiveThemesAsync(CancellationToken cancellationToken);
}
