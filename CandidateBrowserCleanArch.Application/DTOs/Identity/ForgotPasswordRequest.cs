using System.ComponentModel.DataAnnotations;

namespace CandidateBrowserCleanArch.Application;

public class ForgotPasswordRequest
{
    public string Email { get; set; }
    public string ReturnUrl { get; set; }
}
