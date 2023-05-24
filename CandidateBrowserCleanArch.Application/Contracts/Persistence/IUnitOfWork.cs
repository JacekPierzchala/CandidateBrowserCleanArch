using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface IUnitOfWork:IDisposable
{
    ICandidateRepository CandidateRepository { get; }
    ICompanyRepository CompanyRepository { get; }
    IProjectRepository  ProjectRepository { get; }
    
    ICandidateCompanyRepository CandidateCompanyRepository { get; }
    ICandidateProjectRepository CandidateProjectRepository { get; }
    Task<bool> SaveAsync();
}
