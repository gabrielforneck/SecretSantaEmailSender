using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Friends.Commands;

public class AddFriendCommand : IRequest<Result>
{
    public long SecretSantaID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DestinationLink { get; set; }

    public AddFriendCommand(long secretSantaID, string name, string email, string destinationUrl)
    {
        SecretSantaID = secretSantaID;
        Name = name;
        Email = email;
        DestinationLink = destinationUrl;
    }

    public Result Validate()
    {
        if (SecretSantaID <= 0)
            return Result.Failure("ID do amigo secreto inválido.");

        if (string.IsNullOrWhiteSpace(Name))
            return Result.Failure("Nome do amigo inválido.");

        if (string.IsNullOrWhiteSpace(Email))
            return Result.Failure("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(DestinationLink))
            return Result.Failure("Link de destino inválido.");

        return Result.Success();
    }
}
