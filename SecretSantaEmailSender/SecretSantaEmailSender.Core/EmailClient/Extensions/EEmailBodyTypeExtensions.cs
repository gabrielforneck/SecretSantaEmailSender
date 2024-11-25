using MimeKit.Text;
using SecretSantaEmailSender.Core.EmailClient.Emails;

namespace SecretSantaEmailSender.Core.EmailClient.Extensions;

public static class EEmailBodyTypeExtensions
{
    public static TextFormat ToTextFormat(this EEmailBodyType emailBodyType) => emailBodyType switch
    {
        EEmailBodyType.Html => TextFormat.Html,
        _ => TextFormat.Text
    };
}
