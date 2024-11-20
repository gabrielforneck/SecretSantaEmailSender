using SecretSantaEmailSender.Core.EmailClient.Handler;

namespace SecretSantaEmailSender.Core.EmailClient;

public class GmailClient : EmailClientHandler, IGmailClient
{
    public GmailClient(string host, int port, string from, string password, int timeout = 30000) : base(host, port, from, password, timeout)
    {
    }
}
