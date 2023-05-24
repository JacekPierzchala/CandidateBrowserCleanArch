using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application;

public interface IProjectRepository:IGenericRepository<Project>
{
    Task<IEnumerable<Project>> GetAllActiveProjectsAsync();
}
