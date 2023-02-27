using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class AuthorizationExceptionHandler : IExceptionHandler
{
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.Unauthorized;
    public string GetResult(Exception ex) => JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "failure" });
}
