using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SecretSantaEmailSender.Core.Configurations.Extensions;
using SecretSantaEmailSender.Core.EmailClient.Emails;
using SecretSantaEmailSender.Core.EmailClient.Extensions;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Core.EmailClient.Handler;

public class EmailClientHandler : IEmailClientHandler
{
    private readonly IConfiguration _configuration;

    public EmailClientHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Result> Send(IEmail email, CancellationToken cancellationToken)
    {
        var emailConfigurationResult = _configuration.GetEmailConfiguration();
        if (emailConfigurationResult.IsFailure)
            return emailConfigurationResult;

        var emailConfiguration = emailConfigurationResult.Value!;

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(string.Empty, emailConfiguration.From));

        foreach (var to in email.To)
            message.To.Add(new MailboxAddress(string.Empty, to));

        foreach (var cc in email.Cc)
            message.Cc.Add(new MailboxAddress(string.Empty, cc));

        message.Subject = email.Subject;
        message.Body = new TextPart(format: email.BodyType.ToTextFormat()) { Text = email.Body };

        var client = new SmtpClient()
        {
            Timeout = emailConfiguration.Timeout
        };

        try
        {
            client.ConnectAsync(emailConfiguration.Host, emailConfiguration.Port, options: SecureSocketOptions.Auto, cancellationToken: cancellationToken).GetAwaiter().GetResult();
            client.AuthenticateAsync(emailConfiguration.From, emailConfiguration.Password, cancellationToken: cancellationToken).GetAwaiter().GetResult();

            client.SendAsync(message).GetAwaiter().GetResult();
            client.DisconnectAsync(true, cancellationToken: cancellationToken).GetAwaiter().GetResult();
            //TODO: Ver porque o await não funciona
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

        return Result.Success();
    }
}
