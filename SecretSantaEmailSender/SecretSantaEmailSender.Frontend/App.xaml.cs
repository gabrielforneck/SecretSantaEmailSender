using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Frontend.DependencyInjections;
using SecretSantaEmailSender.Frontend.Views.MainView.Windows;
using SecretSantaEmailSender.Infraestructure.SchemaHandlers.Services;
using System.IO;
using System.Reflection;
using System.Windows;

namespace SecretSantaEmailSender.Frontend;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        try
        {
            var appConfiguration = GetConfiguration();
            var serviceColletion = new ServiceCollection();
            DependencyInjector.RegisterServices(serviceColletion, appConfiguration);
            _serviceProvider = serviceColletion.BuildServiceProvider();
        }
        catch (Exception ex)
        {
            ShowFatalError(ex);
            Environment.Exit(-1);
        }
    }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
            .AddJsonFile("appsettings.json", false, false)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        try
        {
            var schemaServices = _serviceProvider.GetRequiredService<IShemaHandlersServices>();
            schemaServices.CreateSchema().Wait();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            ShowFatalError(ex);
            Environment.Exit(-1);
        }
    }

    private static void ShowFatalError(Exception exception)
    {
        MessageBox.Show(exception.Message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
    }
}
