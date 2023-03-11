using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence.IntegrationTests
{
    [TestClass]
    public class CandidatesBrowserDbContextTests
    {
        private CandidatesBrowserDbContext _dbContext;
        public CandidatesBrowserDbContextTests()
        {
            var dbOptions= new DbContextOptionsBuilder<CandidatesBrowserDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())               
                .Options;
            
            _dbContext = new CandidatesBrowserDbContext(dbOptions);
            _dbContext.Candidates.AddRange(CandidatesData.Candidates);
            _dbContext.SaveChanges();


        }

        [TestMethod]
        public async Task AddTest()
        {
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
             _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();
            var volCandidatesNew = await _dbContext.Candidates.CountAsync();

            Assert.IsNotNull(candidate);
            Assert.AreEqual(candidate.Id, maxId);
            Assert.AreEqual(volCandidatesNew, volCandidatesOld+1);
        }

        [TestMethod]
        public async Task UpdateDataTest()
        {
            var candidateToUpdate= await _dbContext.Candidates.FindAsync(1);
            candidateToUpdate.FirstName = "Test";
            var result=await _dbContext.SaveChangesAsync();
            candidateToUpdate = await _dbContext.Candidates.FindAsync(1);

            Assert.IsNotNull(candidateToUpdate);
            Assert.AreEqual(result,1);
            Assert.AreEqual(candidateToUpdate.FirstName, "Test");
        }

        [TestMethod]
        public async Task RemoveDataTest()
        {
            var volCandidatesOld = await _dbContext.Candidates.CountAsync(c=>!c.Deleted);
            var candidateToDelete = await _dbContext.Candidates.FindAsync(1);
            candidateToDelete.Deleted=true;
            await _dbContext.SaveChangesAsync();
            candidateToDelete = await _dbContext.Candidates.FindAsync(1);
            var volCandidatesNew = await _dbContext.Candidates.CountAsync(c => !c.Deleted);

            Assert.AreEqual(candidateToDelete.Deleted, true);
            Assert.AreEqual(volCandidatesNew, volCandidatesOld - 1);
        }
    }
}
