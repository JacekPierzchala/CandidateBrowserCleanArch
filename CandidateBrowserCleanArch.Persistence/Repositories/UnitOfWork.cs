using CandidateBrowserCleanArch.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CandidatesBrowserDbContext _dbContext;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProjectRepository _projectRepository;

    public UnitOfWork(CandidatesBrowserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ICandidateRepository CandidateRepository => _candidateRepository?? new CandidateRepository(_dbContext);

    public ICompanyRepository CompanyRepository => _companyRepository ?? new CompanyRepository(_dbContext);

    public IProjectRepository ProjectRepository => _projectRepository?? new ProjectRepository(_dbContext);  

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch 
        {
            return false;

        }

    }
}
