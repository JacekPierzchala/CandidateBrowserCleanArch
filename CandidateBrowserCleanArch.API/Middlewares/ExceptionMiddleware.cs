using CandidateBrowserCleanArch.Application;
using Newtonsoft.Json;

using System.Net;

namespace CandidateBrowserCleanArch.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger)
	{
        _request = request;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string result = string.Empty;

        IExceptionHandler handler;
        switch (ex)
        {
            case BadRequestException :
                handler = new BadRequestExceptionHandler();
                break;
            case ValidationException:
                handler = new ValidationExceptionHandler();
                break;
            case NotFoundException notFound:
                handler = new NotFoundExceptionHandler();
                break;
            default:
                handler = new InternalServerErrorHandler();
                _logger.LogError(ex.Message);
                break;
        }

        statusCode = handler.GetStatusCode(ex);
        result = handler.GetResult(ex);

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(result);
    }
}


public class ErrorDetails
{
    public string ErrorType { get; set; }
    public string ErrorMessage { get; set; }
}

