using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateBrowserCleanArch.Tests.MockData.Mocks;
using CandidateBrowserCleanArch.Domain;

namespace CandidateBrowserCleanArch.Application.Test;

internal class CandidatesResponsesData
{
    internal static PagedResultResponse<Candidate> ResultResponse(CandidateQueryParameters queryParameters)
    {
        PagedResultResponse<Candidate> response = new()
        {
            TotalCount = CandidatesData.Candidates()
                    .Where(c =>
                    !c.Deleted &&
                           (string.IsNullOrEmpty(queryParameters.FirstName) || c.FirstName.ToLower().Contains(queryParameters.FirstName))
                        && (string.IsNullOrEmpty(queryParameters.LastName) || c.LastName.ToLower().Contains(queryParameters.LastName))
                    )
                    .Where(c => (queryParameters.Companies == null || c.Companies.Any(co => queryParameters.Companies.Any(q => q == co.CompanyId))) &&
                              (queryParameters.Projects == null || c.Projects.Any(co => queryParameters.Projects.Any(q => q == co.ProjectId))))
                    .Count(),
            Items = CandidatesData.Candidates()
                    .Where(c =>
                    !c.Deleted &&
                           (string.IsNullOrEmpty(queryParameters.FirstName) || c.FirstName.ToLower().Contains(queryParameters.FirstName))
                        && (string.IsNullOrEmpty(queryParameters.LastName) || c.LastName.ToLower().Contains(queryParameters.LastName))
                    )
                    .Where(c => (queryParameters.Companies == null || c.Companies.Any(co => queryParameters.Companies.Any(q => q == co.CompanyId))) &&
                                (queryParameters.Projects == null || c.Projects.Any(co => queryParameters.Projects.Any(q => q == co.ProjectId))))
                    .Skip(queryParameters.PageSize * (queryParameters.PageNumber - 1))
                      .Take(queryParameters.PageSize)
                      .ToList(),
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize
        };

        return response;
    }


}
