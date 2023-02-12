using CandidateBrowserCleanArch.Application.DTOs;
using CandidateBrowserCleanArch.Application.Responses;
using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Contracts.Persistence;

public interface ICandidateRepository: IGenericRepository<Candidate>
{
    Task<PagedResultResponse<Candidate>> GetAllActiveCandidatesWithDetailsAsync(CandidateQueryParameters QueryParameters);
    Task<Candidate> GetCandidateWithDetailsAsync(int id);
}
