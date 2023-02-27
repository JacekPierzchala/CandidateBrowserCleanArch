using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class RegistrationException:ApplicationException
{
    public List<string> Errors { get; set; } = new();
    public RegistrationException(List<string> errors)
	{
        errors.ForEach(e => Errors.Add(e)); 
	}
}
