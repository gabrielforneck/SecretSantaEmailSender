using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSantaEmailSender.Core.EmailClient;
using SecretSantaEmailSender.Core.EmailClient.Handler;
using SecretSantaEmailSender.Frontend.DependencyInjections.Exceptions;

namespace SecretSantaEmailSender.Frontend.DependencyInjections;

public static class EmailClientInjection
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        var emailSection = configuration.GetRequiredSection("Email");

        var host = emailSection.GetValue<string>("Host");
        var port = emailSection.GetValue<int>("Port");
        var from = emailSection.GetValue<string>("From");
        var password = emailSection.GetValue<string>("Password");
        var timeout = emailSection.GetValue<int?>("Timeout");

        if (string.IsNullOrWhiteSpace(host))
            throw new BadConfigurationException("E-mail host not specified.");

        if (port == 0)
            throw new BadConfigurationException("E-mail port not specified.");

        if (string.IsNullOrWhiteSpace(from))
            throw new BadConfigurationException("E-mail from e-mail not specified.");

        if (string.IsNullOrWhiteSpace(password))
            throw new BadConfigurationException("E-mail from password not specified.");

        services.AddScoped<IEmailClientHandler>(p => new EmailClientHandler(host, port, from, password, timeout ?? 30000));
    }
}
