using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.SecretFriends.Repository;

public interface ISecretFriendRepository : IRepository
{
    Task DeleteByFriendID(long friendID, CancellationToken cancellationToken);
    Task DeleteBySecretFriendID(long secretFriendID, CancellationToken cancellationToken);
}