using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class BadRequestExceptionHandler:IExceptionHandler
{
 
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.BadRequest;
    public string GetResult(Exception ex) => JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "failure" });
}
