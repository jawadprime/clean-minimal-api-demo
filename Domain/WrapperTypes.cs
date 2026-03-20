namespace Domain;

public abstract record ProductId;
public record HasProductId(Guid Value) : ProductId;
public record NoProductId() : ProductId;

public record ProductName(string Value);

public abstract record ProductCreatedAt();
public record HasProductCreatedAt(DateTime Value) : ProductCreatedAt;
public record NoProductCreatedAt() : ProductCreatedAt;
