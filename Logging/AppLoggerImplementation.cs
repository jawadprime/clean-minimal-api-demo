using Microsoft.Extensions.Options;
using Serilog.Events;

namespace Logging;

public sealed class AppLogger<T> : IAppLogger<T>
{
    private readonly Serilog.ILogger _logger;
    private readonly string _sourceName;

    public AppLogger(Serilog.ILogger logger, IOptions<AppLoggingOptions> options)
    {
        _logger = logger.ForContext<T>();
        _sourceName = options.Value.SourceName;
    }

    private void Write(LogEventLevel level, string message, Exception? ex, bool isGdprSafe)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        _logger
            .ForContext("IsGdprSafe", isGdprSafe)
            .ForContext("Source", _sourceName)
            .Write(level, ex, message);
    }

    public void Debug(string message, Exception? ex = null, bool isGdprSafe = false) =>
        Write(LogEventLevel.Debug, message, ex, isGdprSafe);

    public void Information(string message, Exception? ex = null, bool isGdprSafe = false) =>
        Write(LogEventLevel.Information, message, ex, isGdprSafe);

    public void Warning(string message, Exception? ex = null, bool isGdprSafe = false) =>
        Write(LogEventLevel.Warning, message, ex, isGdprSafe);

    public void Error(string message, Exception? ex = null, bool isGdprSafe = false) =>
        Write(LogEventLevel.Error, message, ex, isGdprSafe);

    public void Fatal(string message, Exception ex, bool isGdprSafe = false) =>
        Write(LogEventLevel.Fatal, message, ex, isGdprSafe);
}