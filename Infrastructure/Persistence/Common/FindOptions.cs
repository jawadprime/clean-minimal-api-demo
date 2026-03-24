namespace Application.Repositories;

public class FindOptions
{
    public bool IsIgnoreAutoIncludes { get; set; } = true;
    public bool IsAsNoTracking { get; set; } = true;
}