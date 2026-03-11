namespace Common.Errors;

public record NotFoundError(
    IReadOnlyDictionary<string, string[]> Failures)
    : HasError("Validation Failed", new NoException());