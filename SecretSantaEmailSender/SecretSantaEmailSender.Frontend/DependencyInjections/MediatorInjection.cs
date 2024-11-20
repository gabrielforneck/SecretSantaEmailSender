using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Application.SecretSantas.Commands;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class MediatorInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSecretSantaCommand).Assembly));
    }
}
