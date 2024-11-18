using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Application.SecretSantas.Repository
{
    public interface ISecretSantaRepository : IRepository
    {
        Task Insert(SecretSanta secretSanta, CancellationToken cancellationToken);
    }
}