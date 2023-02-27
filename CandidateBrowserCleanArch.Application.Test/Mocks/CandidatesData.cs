using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.Test;

internal class CandidatesData
{
   
    internal static List<Candidate> Candidates = new()
    {


                new Candidate
                {
                   Deleted= false,
                   Id= 1,
                   FirstName="John",LastName="Doe",
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 1,
                             CandidateId= 1,
                             CompanyId= 1,
                             Company= new Company
                             {
                                  Id= 1,
                                  CompanyName="Test1"
                             }
                        },
                        new CandidateCompany
                        {
                             Id= 2,
                             CandidateId= 1,
                             CompanyId= 2,
                             Company= new Company
                             {
                                  Id= 1,
                                  CompanyName="Best1"
                             }
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 1,
                             CandidateId= 1,
                             Project= new Project
                             {
                                  Id= 1,
                                  ProjectName="Project Test1"
                             }
                       },
                       new CandidateProject
                       {
                             Id= 2,
                             CandidateId= 1,
                             Project= new Project
                             {
                                  Id= 2,
                                  ProjectName="Project Test2"
                             }
                       }
                   }

                },
                new Candidate
                {
                   Deleted= false,
                   Id= 2,
                   FirstName="John",LastName="Smith",
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 1,
                             CandidateId= 2,
                             CompanyId= 1,
                             Company= new Company
                             {
                                  Id= 1,
                                  CompanyName="Test1"
                             }
                        },
                        new CandidateCompany
                        {
                             Id= 2,
                             CandidateId= 2,
                             CompanyId= 3,
                             Company= new Company
                             {
                                  Id= 1,
                                  CompanyName="Acme"
                             }
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 1,
                             CandidateId= 1,
                             Project= new Project
                             {
                                  Id= 3,
                                  ProjectName="Project Test3"
                             }
                       }

                   }

                },
                new Candidate
                {
                   Deleted= false,
                   Id= 3,
                   FirstName="Kerry",LastName="King",
                },
                new Candidate
                {
                   Deleted= false,
                   Id= 4,
                   FirstName="James",LastName="Hetfield",
                },
                new Candidate
                {
                   Deleted= false,
                   Id= 5,
                   FirstName="Bolesław",LastName="Chrobry",
                },
                new Candidate
                {
                   Deleted= false,
                   Id= 6,
                   FirstName="Bilbo",LastName="Baggins",
                },
                new Candidate
                {
                   Deleted= true,
                   Id= 6,
                   FirstName="Bilbo",LastName="Baggins",
                }


    };

    internal static PagedResultResponse<Candidate> ResultResponse(CandidateQueryParameters queryParameters)
    {
        PagedResultResponse<Candidate> response = new()
        {
            TotalCount = Candidates
                    .Where(c =>
                    !c.Deleted &&
                           (string.IsNullOrEmpty(queryParameters.FirstName) || c.FirstName.ToLower().Contains(queryParameters.FirstName))
                        && (string.IsNullOrEmpty(queryParameters.LastName) || c.LastName.ToLower().Contains(queryParameters.LastName))
                    )
                    .Where(c => (queryParameters.Companies == null || c.Companies.Any(co => queryParameters.Companies.Any(q => q == co.CompanyId))) &&
                              (queryParameters.Projects == null || c.Projects.Any(co => queryParameters.Projects.Any(q => q == co.ProjectId))))
                    .Count(),
            Items = CandidatesData.Candidates
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
