using CandidateBrowserCleanArch.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly CandidatesBrowserDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICandidateCompanyRepository _candidateCompanyRepository;
    private readonly ICandidateProjectRepository _candidateProjectRepository;

    public UnitOfWork(CandidatesBrowserDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    public ICandidateRepository CandidateRepository => _candidateRepository?? new CandidateRepository(_dbContext);

    public ICompanyRepository CompanyRepository => _companyRepository ?? new CompanyRepository(_dbContext);

    public IProjectRepository ProjectRepository => _projectRepository?? new ProjectRepository(_dbContext);  
    public ICandidateCompanyRepository CandidateCompanyRepository => _candidateCompanyRepository ?? new CandidateCompanyRepository(_dbContext);  
    public ICandidateProjectRepository CandidateProjectRepository => _candidateProjectRepository ?? new CandidateProjectRepository(_dbContext);  

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async Task<bool> SaveAsync()
    {
        var userName = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
        return await _dbContext.SaveChangesAsync(userName);
        //try
        //{
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}
        //catch 
        //{
        //    return false;
        //}

    }
}
