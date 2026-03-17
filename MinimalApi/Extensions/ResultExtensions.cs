using Common.Errors;
using Common.Results;
using MinimalApi.V1.Common;

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
            UnexpectedError e => Results.InternalServerError(CreateProblem(e)),
            _ => throw new NotImplementedException($"Error mapping is not handled for {result.Error.GetType()}")
        };
    }

    private static ProblemDetailsResponse CreateProblem(HasError error)
        => new ProblemDetailsResponse(error.Title, string.Join(", ", error.Failures));
}