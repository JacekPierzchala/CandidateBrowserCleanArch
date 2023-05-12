using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Tests.MockData.Mocks
{
    public class CompaniesData
    {
        public static IEnumerable<Company> Companies()
        {
            return new List<Company>()
            {
                            new ()
                            {
                                Id = 1,
                                CompanyName = "Test1"
                            },
                             new ()
                             {
                                  Id= 2,
                                  CompanyName="Best1"
                             },
                             new ()
                             {
                                  Id= 5,
                                  CompanyName="Test1"
                             },
                             new ()
                             {
                                  Id= 3,
                                  CompanyName="Acme"
                             }
            };
        }
    }
}
