using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Logging;

public static class LoggerConfigurator
{
    public static Serilog.ILogger ConfigureLogger(IConfiguration configuration, TelemetryConfiguration telemetryConfiguration, IHostEnvironment env, string logLevel)
    {
        var minimumLevel = DetermineMinimumLogLevel(env.IsDevelopment(), logLevel);

        var config = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .Enrich.FromLogContext()
            .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces);

        if (env.IsDevelopment())
        {
            config.WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
        }

        return config.CreateLogger();
    }

    private static LogEventLevel DetermineMinimumLogLevel(bool isDev, string? logLevel)
    {
        if (string.IsNullOrWhiteSpace(logLevel)) return isDev ? LogEventLevel.Debug : LogEventLevel.Information;
        return Enum.TryParse(logLevel, true, out LogEventLevel level) ? level : (isDev ? LogEventLevel.Debug : LogEventLevel.Information);
    }
}