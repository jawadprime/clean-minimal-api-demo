namespace Common.Errors;

public record ValidationError(
    IReadOnlyDictionary<string, string[]> Failures)
    : HasError("Validation Failed", new NoException());