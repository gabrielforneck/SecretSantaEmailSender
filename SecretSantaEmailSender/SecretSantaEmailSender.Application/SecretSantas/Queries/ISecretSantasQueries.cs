
namespace SecretSantaEmailSender.Application.SecretSantas.Queries;

public interface ISecretSantasQueries
{
    Task<bool> SecretSantaExists(long secretSantaID, CancellationToken cancellationToken);
}