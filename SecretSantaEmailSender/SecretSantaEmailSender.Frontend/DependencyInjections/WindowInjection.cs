using Microsoft.Extensions.DependencyInjection;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class WindowInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
    }
}
