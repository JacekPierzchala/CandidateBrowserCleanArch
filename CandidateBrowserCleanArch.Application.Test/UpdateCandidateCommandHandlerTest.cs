using AutoMapper;
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
    public class UpdateCandidateCommandHandlerTest:CandidatesHandlerTestBase
    {
        private UpdateCandidateCommandHandler _handler;
        private  Mock<IMapper> _mockMapper;
        private Mock<IUnitOfWork> _unitOfWork;
        public UpdateCandidateCommandHandlerTest()
        {
            _mockMapper = new();
            _unitOfWork = new();
            _handler = new(_mockMapper.Object, 
                _unitOfWork.Object);
        }

        [TestMethod]
        public async Task UpdateCandidateTest()
        {
            //Arange
            var request = new UpdateCandidateCommand();
            request.Id= 1;
            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == 1);

            var candidateDTo=_mapperMock.Map<CandidateUpdateDto>(candidate);

         _mockMapper.Setup(x => x.Map<Candidate>(candidateDTo))
         .Returns(candidate);

            _mockMapper.Setup(x => x.Map<CandidateDetailsDto>(candidate))
             .Returns(new CandidateDetailsDto
             {
                 DateOfBirth = candidate.DateOfBirth,
                 FirstName = candidate.FirstName,
                 LastName = candidate.LastName,
                 Email = candidate.Email,
                 Id = candidate.Id
             });

            _candidateRepositoryMock.Setup(x => x.GetCandidateWithDetailsAsync(candidate.Id))
           .ReturnsAsync(candidate);

            _candidateRepositoryMock.Setup(x => x.AddAsync(candidate))
                       .ReturnsAsync(candidate);

            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate.Id)).ReturnsAsync(candidate);
            _unitOfWork.Setup(x => x.CandidateRepository)
              .Returns(_candidateRepositoryMock.Object);

            request.CandidateUpdate = candidateDTo;
            //Act
            var result = await _handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.Id);
            _candidateRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingBadRequest()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 1;
            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == 2);

            var candidateDTo = _mapperMock.Map<CandidateUpdateDto>(candidate);

            _mockMapper.Setup(x => x.Map<Candidate>(candidateDTo))
            .Returns(candidate);

            _mockMapper.Setup(x => x.Map<CandidateDetailsDto>(candidate))
             .Returns(new CandidateDetailsDto
             {
                 DateOfBirth = candidate.DateOfBirth,
                 FirstName = candidate.FirstName,
                 LastName = candidate.LastName,
                 Email = candidate.Email,
                 Id = candidate.Id
             });

            _candidateRepositoryMock.Setup(x => x.GetCandidateWithDetailsAsync(candidate.Id))
           .ReturnsAsync(candidate);

            _candidateRepositoryMock.Setup(x => x.AddAsync(candidate))
                       .ReturnsAsync(candidate);

            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate.Id)).ReturnsAsync(candidate);

            _unitOfWork.Setup(x => x.CandidateRepository)
                          .Returns(_candidateRepositoryMock.Object);

            request.CandidateUpdate = candidateDTo;

            //Act & Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
        
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingValidationException()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 1;
            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == 2);
            candidate.FirstName = "";

            var candidateDTo = _mapperMock.Map<CandidateUpdateDto>(candidate);

            _mockMapper.Setup(x => x.Map<Candidate>(candidateDTo))
            .Returns(candidate);

            _mockMapper.Setup(x => x.Map<CandidateDetailsDto>(candidate))
             .Returns(new CandidateDetailsDto
             {
                 DateOfBirth = candidate.DateOfBirth,
                 FirstName = candidate.FirstName,
                 LastName = candidate.LastName,
                 Email = candidate.Email,
                 Id = candidate.Id
             });

            _candidateRepositoryMock.Setup(x => x.GetCandidateWithDetailsAsync(candidate.Id))
           .ReturnsAsync(candidate);

            _candidateRepositoryMock.Setup(x => x.AddAsync(candidate))
                       .ReturnsAsync(candidate);

            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate.Id)).ReturnsAsync(candidate);

            request.CandidateUpdate = candidateDTo;

            //Act & Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingNotFoundException()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 20;
            var candidate = CandidatesData.Candidates.FirstOrDefault(x => x.Id == 1);
            candidate.Id = 20;

            var candidateDTo = _mapperMock.Map<CandidateUpdateDto>(candidate);

            _mockMapper.Setup(x => x.Map<Candidate>(candidateDTo))
            .Returns(candidate);

            _mockMapper.Setup(x => x.Map<CandidateDetailsDto>(candidate))
             .Returns(new CandidateDetailsDto
             {
                 DateOfBirth = candidate.DateOfBirth,
                 FirstName = candidate.FirstName,
                 LastName = candidate.LastName,
                 Email = candidate.Email,
                 Id = candidate.Id
             });

            _candidateRepositoryMock.Setup(x => x.GetCandidateWithDetailsAsync(candidate.Id))
           .ReturnsAsync(candidate);

            _candidateRepositoryMock.Setup(x => x.AddAsync(candidate))
                       .ReturnsAsync(candidate);

            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);
            _candidateRepositoryMock.Setup(c => c.GetAsync(candidate.Id)).ReturnsAsync((Candidate)null);
            _unitOfWork.Setup(x => x.CandidateRepository)
            .Returns(_candidateRepositoryMock.Object);

            request.CandidateUpdate = candidateDTo;

            //Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
