using Common.Errors;
using Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Extensions;

public static class ResultExtensions
{
    public static IResult ToApiResponse<TDomain, TResponse>(
        this Result<TDomain> result,
        Func<TDomain, TResponse> map)
    {
        if (result.IsSuccess)
        {
            var responseData = map(result.Value);
            return Results.Ok(responseData);
        }

        return result.Error switch
        {
            NotFoundError e => Results.NotFound(CreateProblem(e)),
            ValidationError e => Results.BadRequest(CreateProblem(e)),
            UnexpectedError e => Results.Problem(
                title: e.Title,
                detail: string.Join(", ", e.Failures),
                statusCode: StatusCodes.Status500InternalServerError
            ),
            _ => Results.Problem(statusCode: 500)
        };
    }

    private static ProblemDetails CreateProblem(HasError error)
    {
        return new ProblemDetails
        {
            Title = error.Title,
            Detail = string.Join(", ", error.Failures)
        };
    }
}