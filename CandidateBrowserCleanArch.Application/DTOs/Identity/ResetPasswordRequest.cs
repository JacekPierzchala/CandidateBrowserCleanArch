using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ResetPasswordRequest
{
    public string Token { get; set; }
    public string UserId { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmNewPassword { get; set; }

}
