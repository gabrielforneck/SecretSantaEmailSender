namespace SecretSantaEmailSender.Core.EmailClient.Emails;

public interface IEmail
{
    public string Subject { get; }
    public string Body { get; }
    public EEmailBodyType BodyType { get; }
    public IEnumerable<string> To { get; }
    public IEnumerable<string> Cc { get; }
}
