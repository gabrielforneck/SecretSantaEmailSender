using MediatR;
using SecretSantaEmailSender.Application.Email.Model.Enums;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Commands;

public class CreateSecretSantaCommand : IRequest<Result>
{
    public string? Icon { get; set; }
    public string Name { get; set; }
    public EEmailDesignType EmailDesignType { get; set; }
    public string EmailDesignPath { get; set; }
    public string LinkPlaceholder { get; set; }

    public CreateSecretSantaCommand(string? icon, string name, EEmailDesignType emailDesignType, string emailDesignPath, string linkPlaceholder)
    {
        Icon = icon;
        Name = name;
        EmailDesignType = emailDesignType;
        EmailDesignPath = emailDesignPath;
        LinkPlaceholder = linkPlaceholder;
    }

    public Result Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return Result.Failure("Nome inválido.");

        if (Enum.IsDefined(EmailDesignType))
            return Result.Failure("Tipo do design do e-mail inválido.");

        if (string.IsNullOrWhiteSpace(EmailDesignPath))
            return Result.Failure("Cainho do arquivo inválido.");

        if (string.IsNullOrEmpty(LinkPlaceholder))
            return Result.Failure("Texto de substituição do link inválido.");

        return Result.Success();
    }
}
