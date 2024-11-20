using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Application.SecretFriends.Services;
using SecretSantaEmailSender.Infraestructure.SchemaHandlers.Services;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class ServiceInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ISecretFriendsServices, SecretFriendsServices>();
        services.AddScoped<IShemaHandlersServices, ShemaHandlersServices>();
    }
}
