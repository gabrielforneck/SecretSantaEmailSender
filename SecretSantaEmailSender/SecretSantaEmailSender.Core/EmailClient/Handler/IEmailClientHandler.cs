using SecretSantaEmailSender.Core.EmailClient.Emails;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Core.EmailClient.Handler;

public interface IEmailClientHandler
{
    Task<Result> Send(IEmail email, CancellationToken cancellationToken);
}