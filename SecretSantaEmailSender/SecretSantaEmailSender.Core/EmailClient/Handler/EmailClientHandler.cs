using SecretSantaEmailSender.Core.EmailClient.Emails;
using System.Net;
using System.Net.Mail;

namespace SecretSantaEmailSender.Core.EmailClient.Handler;

public class EmailClientHandler : IEmailClientHandler
{
    public string Host => _host;
    public int Port => _port;
    public string From => _from;
    public int Timeout => _timeout;


    private readonly string _host;
    private readonly int _port;
    private readonly string _from;
    private readonly int _timeout;
    private readonly SmtpClient _smtpClient;

    public EmailClientHandler(string host, int port, string from, string password, int timeout = 30000)
    {
        _host = host;
        _port = port;
        _from = from;
        _timeout = timeout;

        _smtpClient = new SmtpClient()
        {
            Host = host,
            Port = port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(from, password),
            Timeout = timeout
        };
    }

    public async Task Send(IEmail email, CancellationToken cancellationToken)
    {
        var message = new MailMessage
        {
            From = new(_from)
        };

        foreach (var to in email.To)
            message.To.Add(to);

        foreach (var cc in email.Cc)
            message.CC.Add(cc);

        await Task.Run(() => _smtpClient.Send(message), cancellationToken);
    }

    public void Dispose()
    {
        _smtpClient?.Dispose();

        GC.SuppressFinalize(this);
    }
}
