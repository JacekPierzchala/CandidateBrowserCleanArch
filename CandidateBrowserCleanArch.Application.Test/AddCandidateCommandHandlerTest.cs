using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Test
{
    [TestClass]
    public sealed class AddCandidateCommandHandlerTest : CandidatesHandlerTestBase
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private AddCandidateCommandHandler _handler;
        private Mock<IMapper> _mockMapper;
        public AddCandidateCommandHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new AddCandidateCommandHandler(_mockMapper.Object,
                            _unitOfWork.Object);
        }

        [TestMethod]
        public async Task AddNewCandidateTestSuccess()
        {
            // Arrange
            var request = new AddCandidateCommand
            {
                CreateCandidateDto = new CandidateCreateDto
                {
                    DateOfBirth = DateTime.Now,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "John.Doe@gmail.com",

                }
            };

            var candidate = CandidatesData.Candidates.FirstOrDefault(c => c.Id == 1);
  
            _mockMapper.Setup(x => x.Map<Candidate>(request.CreateCandidateDto))
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

            _candidateRepositoryMock.Setup(x => x.AddAsync(candidate))
                                   .ReturnsAsync(candidate);
            _candidateRepositoryMock.Setup(x => x.GetCandidateWithDetailsAsync(candidate.Id))
                       .ReturnsAsync(candidate);

            _unitOfWork.Setup(x => x.CandidateRepository)
                          .Returns(_candidateRepositoryMock.Object);
            _unitOfWork.Setup(x => x.SaveAsync())
                          .ReturnsAsync(true);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.Id);
            Assert.AreEqual("John", result.Data.FirstName);
             _mockMapper.Verify(x => x.Map<Candidate>(It.IsAny<CandidateCreateDto>()), Times.Once);
            _candidateRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Candidate>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);

        }
        [TestMethod]
        public async Task AddNewCandidateTestValidationFailure()
        {
            // Arrange
            var request = new AddCandidateCommand
            {
                CreateCandidateDto = new CandidateCreateDto
                {
                    DateOfBirth = DateTime.Now,
                    FirstName = "",
                    LastName = "Doe",
                    Email = "John.Doe@gmail.com",

                }
            };

            var candidate = CandidatesData.Candidates.FirstOrDefault(c => c.Id == 1);

            _unitOfWork.Setup(u => u.CandidateRepository).Returns(_candidateRepositoryMock.Object);
            _candidateRepositoryMock.Setup(c => c.AddAsync(candidate)).Verifiable();
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(true);

            // Act &Assert 
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }


}
