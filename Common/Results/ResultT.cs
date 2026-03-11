using Common.Errors;

namespace Common.Results;

public class Result<T> : Result
{
    private readonly T? _value;

    protected Result(T value) : base(true, new NoError())
    {
        _value = value;
    }

    protected Result(Error error) : base(false, error) 
    {
        _value = default;
    }

    public T Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");

    public static Result<T> Success(T value)
        => new(value);

    public static new Result<T> Failure(Error error)
        => new(error);
}