namespace Infrastructure.Options;

public class DatabaseOptions
{
    public static string SectionName = "DatabaseConnectionStrings";
    public required string ApplicationDb { get; init; }
}