
namespace SecretSantaEmailSender.Application.Database.Queries;

public interface IDatabaseQueries
{
    Task<long> GetLastInsertedID(CancellationToken cancellationToken);
}