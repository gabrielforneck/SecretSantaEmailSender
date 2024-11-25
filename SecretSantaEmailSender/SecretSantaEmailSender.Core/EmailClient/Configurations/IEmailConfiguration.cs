namespace SecretSantaEmailSender.Core.EmailClient.Configurations;

public interface IEmailConfiguration
{
    public string Host { get; }
    public int Port { get; }
    public string From { get; }
    public string Password { get; }
    public int Timeout { get; }
}
