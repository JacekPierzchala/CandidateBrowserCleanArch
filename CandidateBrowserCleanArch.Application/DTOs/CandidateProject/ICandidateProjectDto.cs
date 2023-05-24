using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public interface ICandidateProjectDto
{
    int CandidateId { get; set; }
    int ProjectId { get; set; }
}
