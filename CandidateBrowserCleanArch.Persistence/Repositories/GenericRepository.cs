using CandidateBrowserCleanArch.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
{
    protected readonly CandidatesBrowserDbContext _dbContext;

    public GenericRepository(CandidatesBrowserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
