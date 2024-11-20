using Dapper;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.Database.Queries;

public class DatabaseQueries : IDatabaseQueries
{
    private readonly ILocalDatabase _localDatabase;

    public DatabaseQueries(ILocalDatabase localDatabase)
    {
        _localDatabase = localDatabase;
    }

    public async Task<long> GetLastInsertedID(CancellationToken cancellationToken)
    {
        const string sql = @"select last_insert_rowid()";

        var command = new CommandDefinition(sql, transaction: _localDatabase.Transaction, cancellationToken: cancellationToken);
        return await _localDatabase.Connection.QuerySingleOrDefaultAsync(command);
    }
}
