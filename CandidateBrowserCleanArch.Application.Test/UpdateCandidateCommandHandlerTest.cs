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

        public UpdateCandidateCommandHandlerTest()
        {
            initMapper();
        }
        [TestMethod]
        public async Task UpdateCandidateTest()
        {
            //Arange

            var request = new UpdateCandidateCommand();
            request.Id= 1;
            CandidateRepositoryMock.CandidateId = request.Id;

            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            request.CandidateUpdate = new()
            {

                Id = request.Id,
                DateOfBirth = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Email = "John.Doe@gmail.com",

            };
            _handler = new(_mapper,_unitOfWork.Object, _pictureStorageServiceMock.Object);

            //Act
            var result = await _handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.Id);
            _unitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingBadRequest()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 1;
            CandidateRepositoryMock.CandidateId = 2;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();
            request.CandidateUpdate = new CandidateUpdateDto
            {
                Id = CandidateRepositoryMock.CandidateId,
                DateOfBirth = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Email = "John.Johnsom@gmail.com",
            };

            _handler = new(_mapper, _unitOfWork.Object, _pictureStorageServiceMock.Object);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<BadRequestException>(() => _handler.Handle(request, CancellationToken.None));
        
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingValidationException()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 1;
            CandidateRepositoryMock.CandidateId = request.Id;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();

            request.CandidateUpdate = new CandidateUpdateDto
            {
                Id = CandidateRepositoryMock.CandidateId,
                DateOfBirth = DateTime.Now,
                FirstName = "",
                LastName = "Doe",
                Email = "John.Johnsom@gmail.com",
            }; 
            _handler = new(_mapper, _unitOfWork.Object, _pictureStorageServiceMock.Object);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [TestMethod]
        public async Task UpdateCandidateTestThrowingNotFoundException()
        {
            var request = new UpdateCandidateCommand();
            request.Id = 20;
            CandidateRepositoryMock.CandidateId = request.Id;
            _unitOfWork = CandidateRepositoryMock.GetUnitofWork();           
            request.CandidateUpdate = new CandidateUpdateDto
            {
                Id = request.Id,
                DateOfBirth = DateTime.Now,
                FirstName = "John",
                LastName = "Johnson",
                Email = "John.Johnsom@gmail.com",
            }; ;

            _handler = new(_mapper, _unitOfWork.Object, _pictureStorageServiceMock.Object);

            //Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
