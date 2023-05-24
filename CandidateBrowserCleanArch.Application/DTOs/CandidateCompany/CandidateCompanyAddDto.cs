using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class CandidateCompanyAddDto : ICandidateCompanyDto
{
    public int CandidateId { get; set; }
    public int CompanyId { get; set; }
    public DateTime DateStart { get; set; }
    public string Position { get; set; }
    public DateTime? DateEnd { get; set; }
}
