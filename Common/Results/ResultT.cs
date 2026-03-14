using Common.Errors;

namespace Common.Results;

public class Result<T> : Result
{
    private readonly T? _value;

    public Result(T value) : base(true, new NoError())
    {
        _value = value;
    }

    public Result(Error error) : base(false, error) 
    {
        _value = default;
    }

    public T Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");
}