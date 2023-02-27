using CandidateBrowserCleanArch.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.UnitTests;

public class MockCandidateRepository
{
    public static CandidateQueryParameters queryParameters = new();
    public static Mock<ICandidateRepository> GetMockCandidateRepository()
    {
       
        var mockRepo= new Mock<ICandidateRepository>();

       
        mockRepo.Setup(repo => repo.GetAllActiveCandidatesWithDetailsAsync(queryParameters))
                .ReturnsAsync(
                    CandidatesData.ResultResponse(queryParameters)
                );



        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Candidate>()))
            .Returns((Candidate candidate) =>
            {
                CandidatesData.Candidates.Add(candidate);
                return Task.FromResult(candidate);
            });

        return mockRepo;

    }


}
