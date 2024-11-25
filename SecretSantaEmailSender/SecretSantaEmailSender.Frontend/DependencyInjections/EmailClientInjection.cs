using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Core.EmailClient.Handler;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class EmailClientInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IEmailClientHandler, EmailClientHandler>();
    }
}
