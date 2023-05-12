
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CandidateBrowserCleanArch.Application;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CandidateBrowserCleanArch.Infrastructure;

public class EmailSenderService : IEmailSenderService
{
    private readonly MailJetSettings _mailjetSettings;
    public EmailSenderService(IOptions<MailJetSettings> mailjetSettings)
    {
        _mailjetSettings = mailjetSettings.Value;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            MailjetClient client = new(_mailjetSettings.MailJetApiKey, _mailjetSettings.MailJetSecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
           .Property(Send.FromEmail, _mailjetSettings.MailJetEmail)
           .Property(Send.FromName, _mailjetSettings.MailJetSender)
           .Property(Send.Subject, subject)
           .Property(Send.HtmlPart, htmlMessage)
           .Property(Send.Recipients,
                new JArray
                {
               new JObject {{"Email", email}}
                });
            ;
            var result = await client.PostAsync(request);
            return true;
        }
        catch 
        {
            return false;
        }

    }
}
