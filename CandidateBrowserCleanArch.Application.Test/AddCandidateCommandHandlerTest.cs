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
        private AddCandidateCommandHandler _handler;
        public AddCandidateCommandHandlerTest()
        {
            _mockMapper = new(); 
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

            CandidateRepositoryMock.CandidateId = 1;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            var candidate = CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateRepositoryMock.CandidateId);
  
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
            // Act
            _handler = new AddCandidateCommandHandler(_mockMapper.Object,_unitOfWork.Object);
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.Id);
            Assert.AreEqual("John", result.Data.FirstName);
             _mockMapper.Verify(x => x.Map<Candidate>(It.IsAny<CandidateCreateDto>()), Times.Once);
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
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            var candidate = CandidatesData.Candidates.FirstOrDefault(c => c.Id == 1);
            _handler = new AddCandidateCommandHandler(_mockMapper.Object, _unitOfWork.Object);

            // Act &Assert 
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }


}
