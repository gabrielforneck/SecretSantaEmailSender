using SecretSantaEmailSender.Core.EmailClient.Emails;

namespace SecretSantaEmailSender.Core.EmailClient.Handler;

public interface IEmailClientHandler : IDisposable
{
    string From { get; }
    string Host { get; }
    int Port { get; }
    int Timeout { get; }

    Task Send(IEmail email, CancellationToken cancellationToken);
}