using Application.Orchestrators;

namespace MinimalApi.Bootstrap;

public static partial class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductOrchestrator, ProductOrchestrator>();

        return services;
    }
}