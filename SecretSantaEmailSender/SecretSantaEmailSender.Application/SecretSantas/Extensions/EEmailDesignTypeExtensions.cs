using SecretSantaEmailSender.Application.Emails.Model.Enums;
using SecretSantaEmailSender.Core.EmailClient.Emails;

namespace SecretSantaEmailSender.Application.SecretSantas.Extensions;

public static class EEmailDesignTypeExtensions
{
    public static EEmailBodyType ToEmailBodyType(this EEmailDesignType designType) => designType switch
    {
        EEmailDesignType.Html => EEmailBodyType.Html,
        _ => EEmailBodyType.Raw
    };
}
