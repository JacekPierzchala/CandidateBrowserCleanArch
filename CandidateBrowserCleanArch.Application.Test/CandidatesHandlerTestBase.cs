using AutoMapper;
using Moq;

namespace CandidateBrowserCleanArch.Application.Test
{

    public  abstract class CandidatesHandlerTestBase
    {
        protected Mock<ICandidateRepository> _candidateRepositoryMock;
        protected Mock<IUnitOfWork> _unitOfWork;
        protected Mock<IMapper> _mockMapper;

    
    }
}