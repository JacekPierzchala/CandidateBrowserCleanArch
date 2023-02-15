using Newtonsoft.Json;
using System.Net;

namespace CandidateBrowserCleanArch.API;

public class NotFoundExceptionHandler:IExceptionHandler
{ 
    public HttpStatusCode GetStatusCode(Exception ex) => HttpStatusCode.NotFound;
    public string GetResult(Exception ex) => JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "failure" });
}
