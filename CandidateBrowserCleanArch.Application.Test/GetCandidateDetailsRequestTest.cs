using AutoMapper;
using CandidateBrowserCleanArch.Domain;
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
    public sealed class GetCandidateDetailsRequestTest : CandidatesHandlerTestBase
    {
        private GetCandidateDetailsRequestHandler _handler;
        public GetCandidateDetailsRequestTest()
        {
            initMapper();
        }
        [TestMethod]
        public async Task Handle_ReturnsCandidateDetails()
        {
            // Arrange

            var request = new GetCandidateDetailsRequest();
            request.CandidateId = 6;
            CandidateRepositoryMock.CandidateId = request.CandidateId;
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();
            _handler = new GetCandidateDetailsRequestHandler(_candidateRepositoryMock.Object, _mapper, _pictureStorageServiceMock.Object);
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(request.CandidateId, result.Data.Id);
            Assert.IsInstanceOfType(result, typeof(ServiceReponse<CandidateDetailsDto>));
        }

        [TestMethod]
        public async Task Handle_ReturnsNullCandidateDetails()
        {
            // Arrange
            var request = new GetCandidateDetailsRequest();
            request.CandidateId = 10;
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();
            CandidateRepositoryMock.CandidateId = request.CandidateId;
            _handler = new GetCandidateDetailsRequestHandler(_candidateRepositoryMock.Object, _mapper, _pictureStorageServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }
}
