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
   

        [TestMethod]
        public async Task DeleteCandidateCommandTest()
        {
            //Arrange
            var request = new DeleteCandidateCommand();
            request.CandidateId = 1;
            CandidateRepositoryMock.CandidateId = request.CandidateId;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            _handler = new(_unitOfWork.Object);


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
            CandidateRepositoryMock.CandidateId = request.CandidateId;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            _handler = new(_unitOfWork.Object);

            // act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
