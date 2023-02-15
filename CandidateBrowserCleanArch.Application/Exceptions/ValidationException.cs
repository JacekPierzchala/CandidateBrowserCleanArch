using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = new();
    public ValidationException(ValidationResult validationResult)
    {
        validationResult.Errors.ForEach(e => Errors.Add(e.ErrorMessage));
    }
}
