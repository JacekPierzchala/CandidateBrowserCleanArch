using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;

namespace CandidateBrowserCleanArch.Persistence;

public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
{
    public CandidateRepository(CandidatesBrowserDbContext dbContext):
        base(dbContext){}

    public async Task DeleteCandidateAsync(int id)
    {
        var candidate =await GetAsync(id);
        candidate.Deleted= true;
    }

    public async Task<PagedResultResponse<Candidate>> GetAllActiveCandidatesWithDetailsAsync(CandidateQueryParameters queryParameters)
    {
        var totalItems = await _dbContext           
            .Candidates
           // .AsSplitQuery()
            .AsNoTracking()
            .Where(c =>
            !c.Deleted &&
                   (string.IsNullOrEmpty(queryParameters.FirstName) || c.FirstName.ToLower().Contains(queryParameters.FirstName))
                && (string.IsNullOrEmpty(queryParameters.LastName) || c.LastName.ToLower().Contains(queryParameters.LastName))
            )
            .Include(c => c.Companies)
            .ThenInclude(c => c.Company)
            .Include(p => p.Projects)
            .ThenInclude(pr => pr.Project)
            .Where(c => (queryParameters.Companies == null || c.Companies.Any(co => queryParameters.Companies.Any(q => q == co.CompanyId))) &&
                      (queryParameters.Projects == null || c.Projects.Any(co => queryParameters.Projects.Any(q => q == co.ProjectId))))
            .ToListAsync();

        var items =  totalItems
                      .Skip(queryParameters.PageSize * (queryParameters.PageNumber - 1))
                      .Take(queryParameters.PageSize)
                      .ToList();


        return new PagedResultResponse<Candidate>
        {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalCount = totalItems.Count()
        };
     }
    

    public async Task<Candidate> GetCandidateWithDetailsAsync(int id)
    {
        var candidate=await _dbContext.Candidates
            .FirstOrDefaultAsync(c=>c.Id==id);

        if (candidate != null)
        {
            candidate.Companies = await _dbContext.CandidateCompanies.Where(c => c.CandidateId == id)
                 .Include(c => c.Company)
                 .ToListAsync();
            candidate.Projects = await _dbContext.CandidateProjects.Where(c => c.CandidateId == id)
                 .Include(c => c.Project)
                 .ToListAsync();
        }

        return candidate;

    }
}
