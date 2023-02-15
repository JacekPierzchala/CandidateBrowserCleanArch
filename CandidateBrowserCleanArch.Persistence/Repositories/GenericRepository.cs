using CandidateBrowserCleanArch.Application;
using Microsoft.EntityFrameworkCore;

namespace CandidateBrowserCleanArch.Persistence;

public class GenericRepository<T> : IGenericRepository<T>
{
    protected readonly CandidatesBrowserDbContext _dbContext;

    public GenericRepository(CandidatesBrowserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Add(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Exists(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
