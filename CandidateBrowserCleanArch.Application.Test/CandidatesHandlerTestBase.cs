using AutoMapper;
using Moq;

namespace CandidateBrowserCleanArch.Application.Test
{

    public  abstract class CandidatesHandlerTestBase
    {
        protected Mock<ICandidateRepository> _candidateRepositoryMock;
        protected Mock<IPictureStorageService> _pictureStorageServiceMock= new();
        protected Mock<IUnitOfWork> _unitOfWork;
        protected  IMapper _mapper;

        protected void initMapper()
        {
            var mapperConfig = new MapperConfiguration(c => {
                c.AddProfile<CandidateProfile>();
                c.AddProfile<CompanyProfile>();
                c.AddProfile<ProjectProfile>();
                c.AddProfile<UserProfile>();

            });
            _mapper = mapperConfig.CreateMapper();
        }
    }
}