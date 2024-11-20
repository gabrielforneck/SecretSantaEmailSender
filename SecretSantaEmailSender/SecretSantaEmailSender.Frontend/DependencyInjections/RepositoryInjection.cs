using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Application.Friends.Repository;
using SecretSantaEmailSender.Application.Raffles.Repository;
using SecretSantaEmailSender.Application.SecretFriends.Repository;
using SecretSantaEmailSender.Application.SecretSantas.Repository;
using SecretSantaEmailSender.Infraestructure.SchemaHandlers.Repositories;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class RepositoryInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IFriendRepository, FriendRepository>();
        services.AddScoped<IRaffleRepository, RaffleRepository>();
        services.AddScoped<ISecretSantaRepository, SecretSantaRepository>();
        services.AddScoped<ISecretFriendRepository, SecretFriendRepository>();
        services.AddScoped<ISchemaHandlerRepository, SchemaHandlerRepository>();
    }
}
