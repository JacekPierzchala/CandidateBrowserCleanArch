using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using Moq;
using System.Reflection.Metadata;

namespace CandidateBrowserCleanArch.Application.Test
{
    [TestClass]
    public class GetActiveCandidatesListRequestHandlerTest
    {
        private Mock<ICandidateRepository> _candidateRepositoryMock;
        private IMapper _mapperMock;
        private GetActiveCandidatesListRequestHandler _handler;
        private CandidateQueryParameters queryParameters;
        public GetActiveCandidatesListRequestHandlerTest()
        {
           _candidateRepositoryMock = new Mock<ICandidateRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapperMock = mapperConfig.CreateMapper();
            queryParameters = new();
            _handler = new GetActiveCandidatesListRequestHandler(_candidateRepositoryMock.Object, _mapperMock);

        }
        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoPage1()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();
            request.QueryParameters = queryParameters;

            var candidateListDtos = new List<CandidateListDto>
            {
                new CandidateListDto {},
                new CandidateListDto {},
                new CandidateListDto {},
                new CandidateListDto {},
                new CandidateListDto {},
            };
            var expectedResult = new PagedResultResponse<CandidateListDto>
            {
                Items = candidateListDtos,
                PageNumber = 1,
                PageSize = 5,
                TotalCount = 6
            };

            _candidateRepositoryMock.Setup(repo => repo.GetAllActiveCandidatesWithDetailsAsync(queryParameters))
            .ReturnsAsync(CandidatesData.ResultResponse(queryParameters));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PagedResultResponse<CandidateListDto>));
            Assert.AreEqual(expectedResult.Items.Count(), result.Items.Count());
            Assert.AreEqual(expectedResult.PageNumber, result.PageNumber);
            Assert.AreEqual(expectedResult.PageSize, result.PageSize);
            Assert.AreEqual(expectedResult.TotalCount, result.TotalCount);
        }

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoPage2()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();

            request.QueryParameters = queryParameters;
            request.QueryParameters.PageNumber = 2;

            var candidateListDtos = new List<CandidateListDto>
            {
                new CandidateListDto {}

            };
            var expectedResult = new PagedResultResponse<CandidateListDto>
            {
                Items = candidateListDtos,
                PageNumber = 2,
                PageSize = 5,
                TotalCount = 6
            };

            _candidateRepositoryMock.Setup(repo => repo.GetAllActiveCandidatesWithDetailsAsync(queryParameters))
            .ReturnsAsync(CandidatesData.ResultResponse(queryParameters));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PagedResultResponse<CandidateListDto>));
            Assert.AreEqual(expectedResult.Items.Count(), result.Items.Count());
            Assert.AreEqual(expectedResult.PageNumber, result.PageNumber);
            Assert.AreEqual(expectedResult.PageSize, result.PageSize);
            Assert.AreEqual(expectedResult.TotalCount, result.TotalCount);
        }
    }
}