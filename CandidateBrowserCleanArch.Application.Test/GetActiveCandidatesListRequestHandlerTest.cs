using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using Moq;
using System.Reflection.Metadata;

namespace CandidateBrowserCleanArch.Application.Test
{
    [TestClass]
    public sealed class GetActiveCandidatesListRequestHandlerTest : CandidatesHandlerTestBase
    {
        private GetActiveCandidatesListRequestHandler _handler;
        public GetActiveCandidatesListRequestHandlerTest()
        {
           initMapper();
        }

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoPage1()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();
            request.QueryParameters = CandidateRepositoryMock.QueryParameters;
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();
          
            // Act
            _handler = new GetActiveCandidatesListRequestHandler(_candidateRepositoryMock.Object, _mapper, _pictureStorageServiceMock.Object);
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PagedResultResponse<CandidateListDto>));
            Assert.AreEqual(5, result.Items.Count());
            Assert.AreEqual(1, result.PageNumber);
            Assert.AreEqual(5, result.PageSize);
            Assert.AreEqual(6, result.TotalCount);
        }

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoPage2()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();

            request.QueryParameters = CandidateRepositoryMock.QueryParameters;
            request.QueryParameters.PageNumber = 2;
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();

            // Act
            _handler = new GetActiveCandidatesListRequestHandler(_candidateRepositoryMock.Object,_mapper, _pictureStorageServiceMock.Object);
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PagedResultResponse<CandidateListDto>));
            Assert.AreEqual(1, result.Items.Count());
            Assert.AreEqual(2, result.PageNumber);
            Assert.AreEqual(5, result.PageSize);
            Assert.AreEqual(6, result.TotalCount);
        }

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoWithCompanyFilter()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();

            request.QueryParameters = CandidateRepositoryMock.QueryParameters;
            request.QueryParameters.PageNumber = 1;
            request.QueryParameters.Companies = new[] { 1 };
            _candidateRepositoryMock = CandidateRepositoryMock.GetCandidateRepository();

            // Act
            _handler = new GetActiveCandidatesListRequestHandler(_candidateRepositoryMock.Object,
                _mapper, _pictureStorageServiceMock.Object);

            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PagedResultResponse<CandidateListDto>));
            Assert.AreEqual(1, result.Items.Count());
            Assert.AreEqual(1, result.PageNumber);
            Assert.AreEqual(5, result.PageSize);
            Assert.AreEqual(1, result.TotalCount);
        }
    }
}