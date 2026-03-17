namespace Logging;
public sealed class AppLoggingOptions
{
    public static string SectionName = "AppLogging";
    public required string SourceName { get; set; }
    public required string LogLevel { get; set; }
}