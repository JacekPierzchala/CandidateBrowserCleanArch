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
            _mockMapper = new();
        }
        [TestMethod]
        public async Task Handle_ReturnsCandidateDetails()
        {
            // Arrange

            var request = new GetCandidateDetailsRequest();
            request.CandidateId = 6;
            CandidateRepositoryMock.CandidateId = request.CandidateId;
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();

            var candidate = CandidatesData.Candidates.FirstOrDefault(c => c.Id == request.CandidateId);        
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
            _handler = new GetCandidateDetailsRequestHandler(_candidateRepositoryMock.Object, _mockMapper.Object);
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
            _handler = new GetCandidateDetailsRequestHandler(_candidateRepositoryMock.Object, _mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

        }
    }
}
