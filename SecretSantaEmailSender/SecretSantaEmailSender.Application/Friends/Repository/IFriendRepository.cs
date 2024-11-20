using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.Friends.Repository;

public interface IFriendRepository : IRepository
{
    Task<Friend?> GetByID(long iD, CancellationToken cancellationToken);
    Task<IEnumerable<Friend>> GetBySecretSantaID(long iD, CancellationToken cancellationToken);
    Task Insert(Friend friend, CancellationToken cancellationToken);
    Task Update(Friend friend, CancellationToken cancellationToken);
    Task Delete(long iD, CancellationToken cancellationToken);
    Task DeleteBySecretSantaID(long secretSantaID, CancellationToken cancellationToken);
}