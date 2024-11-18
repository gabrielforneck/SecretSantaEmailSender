using MediatR;
using SecretSantaEmailSender.Application.Email.Model.Enums;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Commands;

public class UpdateSecretSantaCommand : IRequest<Result>
{
    public long ID { get; set; }
    public string? Icon { get; set; }
    public string Name { get; set; }
    public EEmailDesignType EmailDesignType { get; set; }
    public string EmailDesign { get; set; }
    public string LinkPlaceholder { get; set; }

    public UpdateSecretSantaCommand(long iD, string? icon, string name, EEmailDesignType emailDesignType, string emailDesign, string linkPlaceholder)
    {
        ID = iD;
        Icon = icon;
        Name = name;
        EmailDesignType = emailDesignType;
        EmailDesign = emailDesign;
        LinkPlaceholder = linkPlaceholder;
    }

    public Result Validate()
    {
        if (ID <= 0)
            return Result.Failure("ID do amigo secreto inválido.");

        if (string.IsNullOrWhiteSpace(Name))
            return Result.Failure("Nome inválido.");

        if (Enum.IsDefined(EmailDesignType))
            return Result.Failure("Tipo do design do e-mail inválido.");

        if (string.IsNullOrWhiteSpace(EmailDesign))
            return Result.Failure("Design do e-mail inválido.");

        if (string.IsNullOrEmpty(LinkPlaceholder))
            return Result.Failure("Texto de substituição do link inválido.");

        return Result.Success();
    }
}
