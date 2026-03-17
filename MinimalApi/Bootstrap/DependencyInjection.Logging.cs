using Logging;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;

namespace MinimalApi.Bootstrap;

public static partial class DependencyInjection
{
    public static IServiceCollection AddAppLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AppLoggingOptions>()
            .Bind(configuration.GetSection(AppLoggingOptions.SectionName))
            .ValidateDataAnnotations();

        services.AddSingleton<Serilog.ILogger>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<AppLoggingOptions>>().Value;

            var env = provider.GetRequiredService<IHostEnvironment>();

            var telemetryConfiguration = provider.GetRequiredService<TelemetryConfiguration>();

            return LoggerConfigurator.ConfigureLogger(
                configuration,
                telemetryConfiguration,
                env,
                options.LogLevel);
        });

        services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));

        return services;
    }
}