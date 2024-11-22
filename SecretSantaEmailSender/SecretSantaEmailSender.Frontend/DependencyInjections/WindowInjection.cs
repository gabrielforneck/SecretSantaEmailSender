using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Frontend.Views.MainView.Windows;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class WindowInjection
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
    }
}
