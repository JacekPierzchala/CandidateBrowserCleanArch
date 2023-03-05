using AutoMapper;
using Moq;

namespace CandidateBrowserCleanArch.Application.Test
{

    public  abstract class CandidatesHandlerTestBase
    {
        protected Mock<ICandidateRepository> _candidateRepositoryMock;
        protected IMapper _mapperMock;

        public CandidatesHandlerTestBase()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapperMock = mapperConfig.CreateMapper();
        }
    }
}