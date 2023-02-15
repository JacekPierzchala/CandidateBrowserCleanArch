using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class InternalServerErrorHandler : IExceptionHandler
{
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.InternalServerError;
    public string GetResult(Exception ex) => JsonConvert.SerializeObject(new ErrorDetails
    {
        ErrorMessage ="Something went wrong on server side",
        ErrorType = "failure"
    });
}
