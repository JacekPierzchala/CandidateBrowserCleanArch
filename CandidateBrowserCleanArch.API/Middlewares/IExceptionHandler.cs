using System.Net;

namespace CandidateBrowserCleanArch.API;

public interface IExceptionHandler
{
    HttpStatusCode GetStatusCode(Exception ex);
    string GetResult(Exception ex);
}
