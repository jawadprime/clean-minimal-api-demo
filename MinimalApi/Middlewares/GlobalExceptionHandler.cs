using Microsoft.AspNetCore.Diagnostics;
using MinimalApi.V1.Common;
using FluentValidation;
using Logging;

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
        ProblemDetailsResponse response;
        var statusCode = StatusCodes.Status500InternalServerError;

        switch (exception)
        {
            case BadHttpRequestException:
                statusCode = StatusCodes.Status400BadRequest;
                response = new ProblemDetailsResponse(
                    "Bad Request",
                    "The request could not be processed. Please check your input."
                );
                break;

            case ValidationException validationEx:
                statusCode = StatusCodes.Status400BadRequest;
                response = new ProblemDetailsResponse(
                    "Validation Failed",
                    string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage))
                );
                break;

            default:
                response = new ProblemDetailsResponse(
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