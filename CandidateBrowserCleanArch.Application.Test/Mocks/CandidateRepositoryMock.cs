using CandidateBrowserCleanArch.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Test;

internal  class CandidateRepositoryMock
{
    public static CandidateQueryParameters QueryParameters { get; set; } = new();
    public static int CandidateId { get; set; }
    internal static Mock<ICandidateRepository> GetCandidateRepository()
    {
       var mock = new Mock<ICandidateRepository>();
         
        mock.Setup(repo => repo.GetAllActiveCandidatesWithDetailsAsync(QueryParameters))
        .ReturnsAsync(CandidatesData.ResultResponse(QueryParameters));


        mock.Setup(repo => repo.GetCandidateWithDetailsAsync(CandidateId))
        .ReturnsAsync(CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateId));

        mock.Setup(x => x.AddAsync(new Candidate() ))
            .ReturnsAsync(new Candidate { Id=1 });

        mock.Setup(c => c.GetAsync(CandidateId)).ReturnsAsync(CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateId));

        mock.Setup(c => c.DeleteAsync(CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateId)));

        mock.Setup(x => x.UpdateAsync(CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateId)))
           .ReturnsAsync(CandidatesData.Candidates.FirstOrDefault(c => c.Id == CandidateId));

        return mock;    
    }

    internal  static Mock<IUnitOfWork>GetUnitofWork() 
    { 
        var mock = new  Mock<IUnitOfWork>();
        var _candidateRepositoryMock = GetCandidateRepository();

        mock.Setup(x => x.CandidateRepository)
               .Returns(_candidateRepositoryMock.Object);
        mock.Setup(x => x.SaveAsync())
                      .ReturnsAsync(true);

        return mock;

    }

    //public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()

}