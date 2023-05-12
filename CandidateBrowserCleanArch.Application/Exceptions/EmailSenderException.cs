using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class EmailSenderException: ApplicationException
{
    public EmailSenderException(string message) : base(message)
    {

    }
}
