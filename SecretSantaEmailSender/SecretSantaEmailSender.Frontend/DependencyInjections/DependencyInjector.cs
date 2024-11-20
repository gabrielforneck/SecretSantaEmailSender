using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class DependencyInjector
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationInjection.RegisterServices(services, configuration);

        DatabaseInjection.RegisterServices(services, configuration);

        EmailClientInjection.RegisterServices(services, configuration);

        QueryInjection.RegisterServices(services);

        RepositoryInjection.RegisterServices(services);

        ServiceInjection.RegisterServices(services);

        MediatorInjection.RegisterServices(services);

        WindowInjection.RegisterServices(services);
    }
}
