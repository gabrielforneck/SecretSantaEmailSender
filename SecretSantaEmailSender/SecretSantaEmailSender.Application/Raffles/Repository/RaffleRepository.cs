using Dapper;
using SecretSantaEmailSender.Application.Raffles.Domain;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.Raffles.Repository;

public class RaffleRepository : IRaffleRepository
{
    public ILocalDatabase LocalDatabase { get; }

    public RaffleRepository(ILocalDatabase localDatabase)
    {
        LocalDatabase = localDatabase;
    }

    public async Task Insert(Raffle raffle, CancellationToken cancellationToken)
    {
        const string sql = @"insert into raffles (secret_santa_id,
                                                  created_at)
                                          values (@SecretSantaID,
                                                  @CreatedAt)";

        var command = new CommandDefinition(sql, raffle, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
