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

    public const string UserAssignRole = "User.AssignRole";
    public const string UserDelete = "User.Delete";
    public const string UserLock = "User.Lock";
    public const string UserUpdate = "User.Update";
}