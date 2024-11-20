using Dapper;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.SecretSantas.Queries;

public class SecretSantasQueries : ISecretSantasQueries
{
    private readonly ILocalDatabase _localDatabase;

    public SecretSantasQueries(ILocalDatabase localDatabase)
    {
        _localDatabase = localDatabase;
    }

    public async Task<bool> SecretSantaExists(long secretSantaID, CancellationToken cancellationToken)
    {
        const string sql = @"select count(*) > 0
                               from secret_santas
                              where secret_santas.id = @secretSantaID";

        var command = new CommandDefinition(sql, new { secretSantaID }, transaction: _localDatabase.Transaction, cancellationToken: cancellationToken);
        return await _localDatabase.Connection.QuerySingleOrDefaultAsync<bool>(command);
    }
}
