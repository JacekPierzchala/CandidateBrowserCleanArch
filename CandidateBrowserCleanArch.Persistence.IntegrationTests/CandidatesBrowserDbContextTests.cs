
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Persistence.IntegrationTests;

[TestClass]
public sealed class CandidatesBrowserDbContextTests: CandidatesBaseData
{
   
    public CandidatesBrowserDbContextTests()
    {
        _dbContext=CandidatesBrowserDbContextFactory.InitializeAndSeedDbContext();
    }

    [TestMethod]
    public async Task GetAllTest()
    {
        //Act
        var candidates =await _dbContext.Candidates
            .Include(c=>c.Companies)
            .ThenInclude(c=>c.Company)
            .Include(c => c.Projects)
            .ThenInclude(c => c.Project)
            .ToListAsync();

        //Assert
        Assert.IsNotNull(candidates);
        Assert.IsInstanceOfType(candidates,typeof(List<Candidate>));
    }

    [TestMethod]
    public async Task AddTest()
    {
        //Arrange
        var maxId = await _dbContext.Candidates.MaxAsync(c => c.Id);
        var volCandidatesOld = await _dbContext.Candidates.CountAsync();
        var candidate = new Candidate
        {
            Id = ++maxId,
            FirstName = "John",
            LastName = "Doe",
            Email = "John.Doe@gmail.com",
            DateOfBirth = DateTime.Now,
        };

        //Act
         _dbContext.Candidates.Add(candidate);
        await _dbContext.SaveChangesAsync();
        var volCandidatesNew = await _dbContext.Candidates.CountAsync();

        //Assert
        Assert.IsNotNull(candidate);
        Assert.AreEqual(candidate.Id, maxId);
        Assert.AreEqual(volCandidatesNew, volCandidatesOld+1);
    }

    [TestMethod]
    public async Task UpdateDataTest()
    {
        //Arrange
        var candidateToUpdate = await _dbContext.Candidates.FindAsync(1);
        candidateToUpdate.FirstName = "Test";

        //Act
        var result =await _dbContext.SaveChangesAsync();
        candidateToUpdate = await _dbContext.Candidates.FindAsync(1);

        //Assert
        Assert.IsNotNull(candidateToUpdate);
        Assert.AreEqual(result,1);
        Assert.AreEqual(candidateToUpdate.FirstName, "Test");
    }

    [TestMethod]
    public async Task RemoveDataTest()
    {
        //Arrange
        var volCandidatesOld = await _dbContext.Candidates.CountAsync(c=>!c.Deleted);
        var candidateToDelete = await _dbContext.Candidates.FindAsync(1);


        candidateToDelete.Deleted=true;

        //Act
        await _dbContext.SaveChangesAsync();
        candidateToDelete = await _dbContext.Candidates.FindAsync(1);
        var volCandidatesNew = await _dbContext.Candidates.CountAsync(c => !c.Deleted);

        //Assert
        Assert.AreEqual(candidateToDelete.Deleted, true);
        Assert.AreEqual(volCandidatesNew, volCandidatesOld - 1);
    }
}
