using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRolesAsync();
}
