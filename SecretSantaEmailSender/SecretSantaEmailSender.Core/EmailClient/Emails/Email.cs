namespace SecretSantaEmailSender.Core.EmailClient.Emails;

public class Email : IEmail
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public EEmailBodyType BodyType { get; set; }
    public IEnumerable<string> To { get; set; }
    public IEnumerable<string> Cc { get; set; }

    public Email(string subject, string body, EEmailBodyType bodyType, IEnumerable<string> to, IEnumerable<string>? cc = null)
    {
        Subject = subject;
        Body = body;
        BodyType = bodyType;
        To = to;
        Cc = cc ?? [];
    }
}
