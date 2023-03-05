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

            _handler = new GetCandidateDetailsRequestHandler(_candidateRepositoryMock.Object, _mapperMock);
        }

        [TestMethod]
        public async Task Handle_ReturnsCandidateDetails()
        {
            // Arrange
            var request = new GetCandidateDetailsRequest();
            request.CandidateId = 6;

            _candidateRepositoryMock.Setup(repo => repo.GetCandidateWithDetailsAsync(request.CandidateId))
            .ReturnsAsync(CandidatesData.Candidates.FirstOrDefault(c=>c.Id== request.CandidateId));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ServiceReponse<CandidateDetailsDto>));
        }

        [TestMethod]
        public async Task Handle_ReturnsNullCandidateDetails()
        {
            // Arrange
            var request = new GetCandidateDetailsRequest();

            request.CandidateId = 10;
            _candidateRepositoryMock.Setup(repo => repo.GetCandidateWithDetailsAsync(request.CandidateId))
            .ReturnsAsync((Candidate)null);


            // Act & Assert
           await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }
}
