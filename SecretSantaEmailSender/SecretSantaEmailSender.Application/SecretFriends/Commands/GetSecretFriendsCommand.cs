using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Commands;

public class GetSecretFriendsCommand : IRequest<Result<IEnumerable<long>>>
{
    public long RaffleID { get; set; }

    public GetSecretFriendsCommand(long raffleID)
    {
        RaffleID = raffleID;
    }
}
