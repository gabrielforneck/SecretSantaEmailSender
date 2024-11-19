using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Friends.Commands;

public class UpdateFriendCommand : IRequest<Result>
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DestinationLink { get; set; }

    public UpdateFriendCommand(long iD, string name, string email, string destinationLink)
    {
        ID = iD;
        Name = name;
        Email = email;
        DestinationLink = destinationLink;
    }

    public Result Validate()
    {
        if (ID <= 0)
            return Result.Failure("ID do amigo inválido.");

        if (string.IsNullOrWhiteSpace(Name))
            return Result.Failure("Nome do amigo inválido.");

        if (string.IsNullOrWhiteSpace(Email))
            return Result.Failure("Email do amigo inválido.");

        if (string.IsNullOrWhiteSpace(DestinationLink))
            return Result.Failure("Link de destino inválido.");

        return Result.Success();
    }
}
