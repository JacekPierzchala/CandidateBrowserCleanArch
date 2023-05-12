using System.Security.Claims;

namespace CandidateBrowserCleanArch.Identity;

public interface IUserServicesManager
{
    Task<(ApplicationUser? user, string validationMessage)> ValidateUserAndToken(string username, string refreshToken);
    Task UpdateUser(ApplicationUser user);
    Task<(ApplicationUser? user, string validationMessage)> ValidateAndLoginUserAsync(string userName, string password);
    Task<(ApplicationUser? user, IList<string> validationMessages)> ValidateAndRegisterUserAsync
        (ApplicationUser user, string password);
    Task<IEnumerable<Claim>> GatherUserClaims(ApplicationUser user);

    Task<(ApplicationUser? user, IList<string> validationMessages)> ValidateExternalProviderUserAsync
     (ApplicationUser user);

    Task<(string encryptedEmailToken, string encryptedUserId, string validationMessage)> CreateConfirmEmailToken(ApplicationUser user);
    Task<(string encryptedEmailToken, string encryptedUserId, string validationMessage)> CreateConfirmEmailToken(string email);

    Task<(string encryptedPasswordToken, string encryptedUserId, string validationMessage)> CreateResetPasswordToken(string email);

    Task<bool> ValidateAndConfirmEmail(string userId, string token);
    Task<(bool result, string message)> ValidateAndResetPassword(string userId, string token, string newPassword);
}


