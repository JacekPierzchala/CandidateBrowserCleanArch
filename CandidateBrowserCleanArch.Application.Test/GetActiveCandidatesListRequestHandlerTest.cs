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
        private CandidateQueryParameters queryParameters;
        private Mock<IMapper> _mockMapper;
        public GetActiveCandidatesListRequestHandlerTest()
        {
            queryParameters = new();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetActiveCandidatesListRequestHandler(_candidateRepositoryMock.Object, _mapperMock);
        }

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoPage1()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();
            request.QueryParameters = queryParameters;

            IEnumerable<CandidateListDto> candidateListDtos = new List<CandidateListDto>
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

        [TestMethod]
        public async Task Handle_ReturnsPagedResultResponseOfCandidateListDtoWithCompanyFilter()
        {
            // Arrange
            var request = new GetActiveCandidatesListRequest();

            request.QueryParameters = queryParameters;
            request.QueryParameters.PageNumber = 1;
            request.QueryParameters.Companies = new[] { 1 };

            var candidateListDtos = new List<CandidateListDto>
            {
                new CandidateListDto
                {

                    Companies= new List<CandidateCompanyDto>()
                    {
                         new CandidateCompanyDto
                         {
                             Company=new() { Id=1}
                         },
                         new CandidateCompanyDto
                         {
                            Company=new(){ Id=2}
                         }
                    }
                },
                new CandidateListDto
                {

                    Companies= new List<CandidateCompanyDto>()
                    {
                         new CandidateCompanyDto
                         {
                             Company=new() { Id=3}
                         },
                         new CandidateCompanyDto
                         {
                            Company=new() { Id=2}
                         }
                    }
                },
                 new CandidateListDto
                {

                    Companies= new List<CandidateCompanyDto>()
                    {
                         new CandidateCompanyDto
                         {
                             Company=new() { Id=4}
                         },
                         new CandidateCompanyDto
                         {
                            Company=new() { Id=5}
                         }
                    }
                }

            };
            var expectedResult = new PagedResultResponse<CandidateListDto>
            {
                Items = candidateListDtos.Where(cand => cand.Companies.Any(com => com.Company.Id == 1)).ToList(),
                PageNumber = 1,
                PageSize = 5,
                TotalCount = candidateListDtos.Count(cand => cand.Companies.Any(com => com.Company.Id == 1))
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