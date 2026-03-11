using Common.Errors;

namespace Common.Results;

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(T value) : base(true, new NoError())
    {
        Value = value;
    }

    protected Result(Error error) : base(false, error)
    {
    }

    public static Result<T> Success(T value)
        => new(value);

    public static new Result<T> Failure(Error error)
        => new(error);
}