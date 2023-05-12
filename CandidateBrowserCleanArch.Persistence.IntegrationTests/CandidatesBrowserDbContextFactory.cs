namespace CandidateBrowserCleanArch.Persistence.IntegrationTests;
internal class CandidatesBrowserDbContextFactory
{
    internal static CandidatesBrowserDbContext InitializeAndSeedDbContext()
    {
        CandidatesBrowserDbContext _dbContext;
        var dbOptions = new DbContextOptionsBuilder<CandidatesBrowserDbContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString())
         .Options;

        _dbContext = new CandidatesBrowserDbContext(dbOptions);
        _dbContext.Candidates.AddRange(CandidatesData.CandidatesWithoutNavigationProperties());
        _dbContext.Companies.AddRange(CompaniesData.Companies());
        _dbContext.Projects.AddRange(ProjectsData.Projects());
        _dbContext.SaveChanges();

        return _dbContext;
    }
}
