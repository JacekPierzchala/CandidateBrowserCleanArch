using CandidateBrowserCleanArch.Application.DTOs.Common;
using CandidateBrowserCleanArch.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.DTOs.CandidateProject
{
    public class CandidateProjectDto:BaseDto
    {
        public ProjectDto Project { get; set; }
    }
}
