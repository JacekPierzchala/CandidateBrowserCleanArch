using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class RegistrationResponse:BaseResponse
{
    public List<string> Errors { get; set; } = new();
    public string UserId { get; set; }
    public string ValidToken { get; set; }
    public string EncryptedUserId { get; set; }
}
