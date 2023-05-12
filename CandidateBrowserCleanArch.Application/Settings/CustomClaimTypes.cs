using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public static class CustomClaimTypes
{
    public  const string Uid = "uid";
    public  const string Permission = "Permission";
    
}

public static class CustomRoleClaims
{
    public const string CandidateCreate = "Candidate.Create";
    public const string CandidateDelete = "Candidate.Delete";
    public const string CandidateRead = "Candidate.Read";
    public const string CandidateUpdate = "Candidate.Update";
}