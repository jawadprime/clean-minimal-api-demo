using Logging;
using Microsoft.AspNetCore.Diagnostics;
using MinimalApi.V1.Common;

namespace MinimalApi.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IAppLogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IAppLogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemInfo response;
        var statusCode = StatusCodes.Status500InternalServerError;

        switch (exception)
        {
            case BadHttpRequestException badHttpRequestEx:
                statusCode = StatusCodes.Status400BadRequest;

                var exceptiondetails = badHttpRequestEx.InnerException != null
                   ? $"{badHttpRequestEx.Message} Details: {badHttpRequestEx.InnerException.Message}"
                   : badHttpRequestEx.Message;

                response = new ProblemInfo(
                    "Bad Request",
                     exceptiondetails
                );
                break;

            default:
                response = new ProblemInfo(
                    "Unexpected Error",
                    "An unexpected error occurred. Please contact support if it persists."
                );
                break;
        }

        _logger.Error("Unhandled exception occurred.", exception);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}