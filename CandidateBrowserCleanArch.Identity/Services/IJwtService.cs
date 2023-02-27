using System.Security.Claims;

namespace CandidateBrowserCleanArch.Identity;

public interface IJwtService
{
    string GenerateToken(IList<Claim> userClaims, 
        IList<string> roles, 
        ApplicationUser user, 
        IEnumerable<string> rolePermissions);
}
