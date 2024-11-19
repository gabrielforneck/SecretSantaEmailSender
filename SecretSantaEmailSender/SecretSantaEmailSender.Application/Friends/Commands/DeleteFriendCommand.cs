using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Friends.Commands;

public class DeleteFriendCommand : IRequest<Result>
{
    public long ID { get; set; }

    public DeleteFriendCommand(long iD)
    {
        ID = iD;
    }

    public Result Validate()
    {
        if (ID <= 0)
            return Result.Failure("ID do amigo inválido.");

        return Result.Success();
    }
}
