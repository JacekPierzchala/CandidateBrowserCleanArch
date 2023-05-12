using System.Security.Claims;

namespace CandidateBrowserCleanArch.Identity;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, IEnumerable<Claim> userClaims);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    string GenerateRefreshToken();


}
