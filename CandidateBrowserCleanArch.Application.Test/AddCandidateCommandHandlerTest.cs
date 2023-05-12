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
   
            initMapper();

        }

        [TestMethod]
        public async Task AddNewCandidateTestSuccess()
        {
            // Arrange
            var request = new AddCandidateCommand();
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            _handler = new AddCandidateCommandHandler(_mapper, _unitOfWork.Object);

            request.CreateCandidateDto = new()
            {
                DateOfBirth = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Email = "John.Doe@gmail.com",

            };

            CandidateRepositoryMock.CandidateId = 1;
            //Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert

            Assert.IsTrue(result.Success);

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
            _handler = new AddCandidateCommandHandler(_mapper, _unitOfWork.Object);

            // Act &Assert 
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }


}
