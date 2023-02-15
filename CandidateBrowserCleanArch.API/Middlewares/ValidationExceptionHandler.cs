using CandidateBrowserCleanArch.Application;
using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class ValidationExceptionHandler : IExceptionHandler
{
  
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.BadRequest;

    public string GetResult(Exception ex) => JsonConvert.SerializeObject(((ValidationException)ex).Errors);
}
