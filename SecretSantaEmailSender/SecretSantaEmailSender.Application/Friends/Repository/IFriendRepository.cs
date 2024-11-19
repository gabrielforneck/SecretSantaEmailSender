using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.Friends.Repository;

public interface IFriendRepository : IRepository
{
    Task Insert(Friend friend, CancellationToken cancellationToken);
}