using CandidateBrowserCleanArch.Application.DTOs.CandidateCompany;
using CandidateBrowserCleanArch.Application.DTOs.CandidateProject;
using CandidateBrowserCleanArch.Application.DTOs.Common;
using CandidateBrowserCleanArch.Application.DTOs.Company;
using CandidateBrowserCleanArch.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.DTOs.Candidate;

public class CandidateListDto:BaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IEnumerable<CandidateCompanyDto> Companies { get; set; }= new List<CandidateCompanyDto>();
    public IEnumerable<CandidateProjectDto> Projects { get; set; }= new List<CandidateProjectDto>();



}
