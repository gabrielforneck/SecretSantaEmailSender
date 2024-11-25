namespace SecretSantaEmailSender.Core.EmailClient.Configurations;

public class EmailConfiguration : IEmailConfiguration
{
    public string Host { get; }
    public int Port { get; }
    public string From { get; }
    public string Password { get; }
    public int Timeout { get; }

    public EmailConfiguration(string host, int port, string from, string password, int timeout = 30000)
    {
        Host = host;
        Port = port;
        From = from;
        Password = password;
        Timeout = timeout;
    }
}
