using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class CandidateProjectUpdateDto:BaseDto,ICandidateProjectDto
{
    public int CandidateId { get; set; }
    public int ProjectId { get; set; }
}
