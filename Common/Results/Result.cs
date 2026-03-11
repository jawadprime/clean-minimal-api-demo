using Common.Errors;

namespace Common.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error is HasError)
            throw new ArgumentException("Invalid error");

        if (!isSuccess && error is NoError)
            throw new ArgumentException("Error required");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true, new NoError());

    public static Result Failure(Error error)
        => new(false, error);
}