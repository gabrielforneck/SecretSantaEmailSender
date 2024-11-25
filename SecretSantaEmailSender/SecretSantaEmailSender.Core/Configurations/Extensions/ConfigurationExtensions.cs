using Microsoft.Extensions.Configuration;
using SecretSantaEmailSender.Core.EmailClient.Configurations;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Core.Configurations.Extensions;

public static class ConfigurationExtensions
{
    public static Result<IEmailConfiguration> GetEmailConfiguration(this IConfiguration configuration)
    {
        var emailSection = configuration.GetRequiredSection("Email");

        var host = emailSection.GetValue<string>("Host");
        var port = emailSection.GetValue<int>("Port");
        var from = emailSection.GetValue<string>("From");
        var password = emailSection.GetValue<string>("Password");
        var timeout = emailSection.GetValue<int?>("Timeout");

        if (string.IsNullOrWhiteSpace(host))
            return Result.Failure<IEmailConfiguration>("Host não especificado.");

        if (port == 0)
            return Result.Failure<IEmailConfiguration>("Pora não especificada.");

        if (string.IsNullOrWhiteSpace(from))
            return Result.Failure<IEmailConfiguration>("E-mail não especificado.");

        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure<IEmailConfiguration>("Senha não especificada.");

        return new EmailConfiguration(host, port, from, password, timeout ?? 30000);
    }
}
