using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandidateBrowserCleanArch.Domain.Common;

namespace CandidateBrowserCleanArch.Domain;

public class CandidateCompany : BaseEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }


    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }
}
