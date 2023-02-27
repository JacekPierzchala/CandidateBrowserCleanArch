using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.UnitTests;

public class GetActiveCandidatesListRequestHandlerTest
{
	private readonly Mock<ICandidateRepository> _mockRepo;
    private IMapper _mapper;
    public GetActiveCandidatesListRequestHandlerTest()
	{
        _mockRepo=MockCandidateRepository.GetMockCandidateRepository();
        var mapperConfig= new MapperConfiguration(c=>
        {
            c.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }
    [Fact]
    public async Task GetActiveCandidatesListTest()
    {
        var handler= new GetActiveCandidatesListRequestHandler(_mockRepo.Object,_mapper);

        //  MockCandidateRepository.queryParameters.Companies=
        var request = new GetActiveCandidatesListRequest();
        request.QueryParameters = new();
          var result = await handler.Handle(request,
            CancellationToken.None);

     

        result.ShouldBeOfType<PagedResultResponse<CandidateListDto>>();

        result.Items.Count().ShouldBe(7);
    }
}
