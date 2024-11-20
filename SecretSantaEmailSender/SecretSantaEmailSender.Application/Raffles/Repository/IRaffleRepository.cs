using SecretSantaEmailSender.Application.Raffles.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.Raffles.Repository;

public interface IRaffleRepository : IRepository
{
    Task Insert(Raffle raffle, CancellationToken cancellationToken);
}