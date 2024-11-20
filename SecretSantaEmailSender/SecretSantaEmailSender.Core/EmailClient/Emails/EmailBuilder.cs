namespace SecretSantaEmailSender.Core.EmailClient.Emails;

public class EmailBuilder
{
    private string Subject { get; set; } = string.Empty;
    private string Body { get; set; } = string.Empty;
    private EEmailBodyType BodyType { get; set; } = EEmailBodyType.Raw;
    private List<string> To { get; set; } = [];
    private List<string> Cc { get; set; } = [];

    public EmailBuilder()
    {
    }

    public EmailBuilder SetSubject(string subject)
    {
        Subject = subject;
        return this;
    }

    public EmailBuilder SetBody(string body, EEmailBodyType bodyType = EEmailBodyType.Raw)
    {
        Body = body;
        return this;
    }

    public EmailBuilder AddTo(string to) => AddTo([to]);

    public EmailBuilder AddTo(List<string> to)
    {
        AddEmail(To, to);
        return this;
    }

    public EmailBuilder RemTo(string to)
    {
        RemEmail(To, to);
        return this;
    }

    public EmailBuilder AddCc(string cc) => AddCc([cc]);

    public EmailBuilder AddCc(List<string> cc)
    {
        AddEmail(Cc, cc);
        return this;
    }

    public EmailBuilder RemCc(string cc)
    {
        RemEmail(Cc, cc);
        return this;
    }

    public IEmail Build()
    {
        if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(Body) || To.Count == 0)
            throw new InvalidOperationException("Not all required fields are filled in.");

        return new Email(Subject, Body, BodyType, To, cc: Cc);
    }

    private static void AddEmail(List<string> emails, List<string> emailsToAdd) => emails.AddRange(emailsToAdd);

    private static void RemEmail(List<string> emails, string email) => emails.Remove(email);
}
