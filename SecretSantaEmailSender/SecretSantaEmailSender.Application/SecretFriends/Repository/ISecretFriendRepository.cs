using SecretSantaEmailSender.Application.SecretFriends.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.SecretFriends.Repository;

public interface ISecretFriendRepository : IRepository
{
    Task Insert(SecretFriend secretFriend, CancellationToken cancellationToken);
    Task DeleteByFriendID(long friendID, CancellationToken cancellationToken);
    Task DeleteBySecretFriendID(long secretFriendID, CancellationToken cancellationToken);
    Task DeleteBySecretSantaID(long secretSantaID, CancellationToken cancellationToken);
}