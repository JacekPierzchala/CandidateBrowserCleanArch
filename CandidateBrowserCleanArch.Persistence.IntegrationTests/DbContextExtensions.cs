using CandidateBrowserCleanArch.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Persistence.IntegrationTests
{
    internal class DbContextExtensions : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasData
                (
    new Candidate
    {
        Deleted = false,
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = "John.Doe@gmail.com",
        DateOfBirth = DateTime.Now,
        Companies = new List<CandidateCompany>
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
                                  Id= 2,
                                  CompanyName="Best1"
                             }
                        }
                   },
        Projects = new List<CandidateProject>
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
                    Deleted = false,
                    Id = 2,
                    FirstName = "John",
                    LastName = "Smith",
                    Companies = new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 1,
                             CandidateId= 2,
                             CompanyId= 5,
                             Company= new Company
                             {
                                  Id= 5,
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
                                  Id= 3,
                                  CompanyName="Acme"
                             }
                        }
                   },
                    Projects = new List<CandidateProject>
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
                    Deleted = false,
                    Id = 3,
                    FirstName = "Kerry",
                    LastName = "King",
                },
                new Candidate
                {
                    Deleted = false,
                    Id = 4,
                    FirstName = "James",
                    LastName = "Hetfield",
                },
                new Candidate
                {
                    Deleted = false,
                    Id = 5,
                    FirstName = "Bolesław",
                    LastName = "Chrobry",
                },
                new Candidate
                {
                    Deleted = false,
                    Id = 6,
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                },
                new Candidate
                {
                    Deleted = true,
                    Id = 6,
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                }
                );

        }
    }
}
