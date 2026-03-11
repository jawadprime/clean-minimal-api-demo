namespace Common.Errors;

public abstract record Error;
public abstract record HasError(string Message, MaybeException Exception) : Error;
public sealed record NoError() : Error;