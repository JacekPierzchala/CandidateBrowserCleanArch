using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class EmailSenderExceptionHandler : IExceptionHandler
{
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.NotImplemented;
    public string GetResult(Exception ex) => JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "failure" });
}
