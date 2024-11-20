using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Commands;

public class SendEmailCommand : IRequest<Result>
{
    public long SecretFriendID { get; set; }

    public SendEmailCommand(long secretFriendID)
    {
        SecretFriendID = secretFriendID;
    }

    public Result Validate()
    {
        if (SecretFriendID <= 0)
            return Result.Failure("ID do amigo secreto inválido.");

        return Result.Success();
    }
}
