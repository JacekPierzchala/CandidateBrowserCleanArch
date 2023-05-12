using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ForgotPasswordResponse:BaseResponse
{
    public string ValidToken { get; set; }
    public string EncryptedUserId { get; set; }
}
