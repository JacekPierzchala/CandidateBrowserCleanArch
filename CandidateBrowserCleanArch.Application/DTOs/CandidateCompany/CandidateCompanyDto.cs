using CandidateBrowserCleanArch.Application.DTOs.Common;
using CandidateBrowserCleanArch.Application.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application.DTOs.CandidateCompany
{
    public class CandidateCompanyDto:BaseDto
    {
        public CompanyDto Company { get; set; }
        public DateTime DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}
