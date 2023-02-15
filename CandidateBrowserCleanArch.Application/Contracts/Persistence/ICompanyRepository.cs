using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface ICompanyRepository:IGenericRepository<Company>
{
    Task<IEnumerable<Company>> GetAllActiveCompaniesAsync();
}
