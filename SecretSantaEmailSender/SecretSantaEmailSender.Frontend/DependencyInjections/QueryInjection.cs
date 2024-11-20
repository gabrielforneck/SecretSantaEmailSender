using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Application.Database.Queries;
using SecretSantaEmailSender.Application.SecretFriends.Queries;
using SecretSantaEmailSender.Application.SecretSantas.Queries;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class QueryInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IDatabaseQueries, DatabaseQueries>();
        services.AddScoped<ISecretSantasQueries, SecretSantasQueries>();
        services.AddScoped<ISecretFriendsQueries, SecretFriendsQueries>();
    }
}
