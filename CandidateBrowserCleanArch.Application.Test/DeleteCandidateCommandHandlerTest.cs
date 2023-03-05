using CandidateBrowserCleanArch.Applicationl;
using CandidateBrowserCleanArch.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Test
{
    [TestClass]
    public class DeleteCandidateCommandHandlerTest:CandidatesHandlerTestBase
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private DeleteCandidateCommandHandler _handler; 
   
        public DeleteCandidateCommandHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler= new (_unitOfWork.Object);
        }

        [TestMethod]
        public async Task DeleteCandidateCommandTest()
        {
            //Arrange
            var request = new DeleteCandidateCommand();
            request.CandidateId = 1;

            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == request.CandidateId);

            _unitOfWork.Setup(u => u.CandidateRepository).Returns(_candidateRepositoryMock.Object);
            _candidateRepositoryMock.Setup(c => c.DeleteAsync(candidate!));
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate.Id)).ReturnsAsync(candidate);
            
            //act
            var result = await _handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteCandidateCommandTestFailedNotFound()
        {
            //Arrange
            var request = new DeleteCandidateCommand();
            request.CandidateId = 20;

            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == request.CandidateId);

            _unitOfWork.Setup(u => u.CandidateRepository).Returns(_candidateRepositoryMock.Object);
            _candidateRepositoryMock.Setup(c => c.DeleteAsync(candidate!));
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate!=null?candidate.Id:0 )).ReturnsAsync((Candidate)null);
 
            // act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
