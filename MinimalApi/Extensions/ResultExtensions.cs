using Common.Errors;
using Common.Results;
using MinimalApi.V1.Common;

namespace MinimalApi.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemResult<TDomain>(this Result<TDomain> result)
    {
        if (result is null) throw new ArgumentNullException(nameof(result));

        return result.Error switch
        {
            NotFoundError e => Results.NotFound(CreateProblem(e)),
            ValidationError e => Results.BadRequest(CreateProblem(e)),
            UnexpectedError e => Results.InternalServerError(CreateProblem(e)),
            _ => throw new NotImplementedException($"Error mapping is not handled for {result.Error.GetType()}")
        };
    }

    public static IResult ToActionResult<TDomain, TResponse>(
        this Result<TDomain> result,
        Func<TDomain, TResponse> map)
    {
        if (result is null) throw new ArgumentNullException(nameof(result));

        return result.IsSuccess
            ? Results.Ok(map(result.Value))
            : result.ToProblemResult();
    }

    private static ProblemInfo CreateProblem(HasError error)
        => new ProblemInfo(error.Title, string.Join(", ", error.Failures));
}