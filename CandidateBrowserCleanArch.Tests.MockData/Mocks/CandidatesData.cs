using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Tests.MockData.Mocks
{
    public class CandidatesData
    {
        public static IEnumerable<Candidate> Candidates()
        {
            return new List<Candidate>()
            {
                new()
                {
                   Deleted= false,
                   Id= 1,
                   FirstName="John",LastName="Doe",
                   Email = "John.Doe@gmail.com",
                   DateOfBirth= DateTime.Now,
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 1,
                             CandidateId= 1,
                             CompanyId= 1,
                             Company =CompaniesData.Companies().FirstOrDefault(c=>c.Id==1),
                        },
                        new CandidateCompany
                        {
                             Id= 2,
                             CandidateId= 1,
                             CompanyId= 2,
                             Company =CompaniesData.Companies().FirstOrDefault(c=>c.Id==2),
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 1,
                             CandidateId= 1,
                             ProjectId=1,
                             Project =ProjectsData.Projects().FirstOrDefault(c=>c.Id==1),
                       },
                       new CandidateProject
                       {
                             Id= 2,
                             CandidateId= 1,
                             ProjectId=2,
                             Project =ProjectsData.Projects().FirstOrDefault(c=>c.Id==2),
                       }
                   }

                },
                new ()
                {
                   Deleted= false,
                   Id= 2,
                   FirstName="John",LastName="Smith",
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 3,
                             CandidateId= 2,
                             CompanyId= 5,
                             Company =CompaniesData.Companies().FirstOrDefault(c=>c.Id==5),
                        },
                        new CandidateCompany
                        {
                             Id= 4,
                             CandidateId= 2,
                             CompanyId= 3,
                             Company =CompaniesData.Companies().FirstOrDefault(c=>c.Id==3),
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 3,
                             CandidateId= 1,
                             ProjectId=3,
                             Project =ProjectsData.Projects().FirstOrDefault(c=>c.Id==3),
                       }

                   }

                },
                new ()
                {
                   Deleted= false,
                   Id= 3,
                   FirstName="Kerry",LastName="King",
                },
                new ()
                {
                   Deleted= false,
                   Id= 4,
                   FirstName="James",LastName="Hetfield",
                },
                new ()
                {
                   Deleted= false,
                   Id= 5,
                   FirstName="Bolesław",LastName="Chrobry",
                },
                new ()
                {
                   Deleted= false,
                   Id= 6,
                   FirstName="Bilbo",LastName="Baggins",
                },
                new ()
                {
                   Deleted= true,
                   Id= 7,
                   FirstName="Bilbo",LastName="Baggins",
                }
            };
        }

        public static IEnumerable<Candidate> CandidatesWithoutNavigationProperties()
        {
            return new List<Candidate>()
            {
                new()
                {
                   Deleted= false,
                   Id= 1,
                   FirstName="John",LastName="Doe",
                   Email = "John.Doe@gmail.com",
                   DateOfBirth= DateTime.Now,
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 1,
                             CandidateId= 1,
                             CompanyId= 1,    
                        },
                        new CandidateCompany
                        {
                             Id= 2,
                             CandidateId= 1,
                             CompanyId= 2,            
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 1,
                             CandidateId= 1,
                             ProjectId=1,
                       },
                       new CandidateProject
                       {
                             Id= 2,
                             CandidateId= 1,
                             ProjectId=2,
                       }
                   }

                },
                new ()
                {
                   Deleted= false,
                   Id= 2,
                   FirstName="John",LastName="Smith",
                   Companies= new List<CandidateCompany>
                   {
                        new CandidateCompany
                        {
                             Id= 3,
                             CandidateId= 2,
                             CompanyId= 5,
                        },
                        new CandidateCompany
                        {
                             Id= 4,
                             CandidateId= 2,
                             CompanyId= 3,
                        }
                   },
                   Projects= new List<CandidateProject>
                   {
                       new CandidateProject
                       {
                             Id= 3,
                             CandidateId= 1,
                             ProjectId=3,
                       }

                   }

                },
                new ()
                {
                   Deleted= false,
                   Id= 3,
                   FirstName="Kerry",LastName="King",
                },
                new ()
                {
                   Deleted= false,
                   Id= 4,
                   FirstName="James",LastName="Hetfield",
                },
                new ()
                {
                   Deleted= false,
                   Id= 5,
                   FirstName="Bolesław",LastName="Chrobry",
                },
                new ()
                {
                   Deleted= false,
                   Id= 6,
                   FirstName="Bilbo",LastName="Baggins",
                },
                new ()
                {
                   Deleted= true,
                   Id= 7,
                   FirstName="Bilbo",LastName="Baggins",
                }
            };
        }
    }
}
