using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CandidateBrowserCleanArch.Application;

public interface IEmailSenderService
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}
