using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class DatabaseInjection
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LocalDatabase");
        services.AddScoped<ILocalDatabase>(p => new LocalDatabase(connectionString!));
    }
}
