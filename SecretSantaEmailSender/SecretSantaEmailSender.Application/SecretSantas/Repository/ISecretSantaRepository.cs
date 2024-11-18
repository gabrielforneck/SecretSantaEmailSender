using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.SecretSantas.Repository
{
    public interface ISecretSantaRepository : IRepository
    {
        Task<SecretSanta?> GetByID(long ID, CancellationToken cancellationToken);
        Task Insert(SecretSanta secretSanta, CancellationToken cancellationToken);
        Task Update(SecretSanta secretSanta, CancellationToken cancellationToken);
    }
}