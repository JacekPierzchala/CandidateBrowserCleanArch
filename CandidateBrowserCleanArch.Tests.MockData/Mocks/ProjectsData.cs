using CandidateBrowserCleanArch.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Tests.MockData.Mocks
{
    public class ProjectsData
    {
        public static IEnumerable<Project> Projects()
        {
            return new List<Project>()
            {
                             new Project
                             {
                                  Id= 1,
                                  ProjectName="Project Test1"
                             },
                             new Project
                             {
                                  Id= 2,
                                  ProjectName="Project Test2"
                             },
                             new Project
                             {
                                  Id= 3,
                                  ProjectName="Project Test3"
                             }
            };
        }
    }
}
