using Dapper;
using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.Friends.Repository;

public class FriendRepository
{
    private readonly ILocalDatabase _localDatabase;

    public FriendRepository(ILocalDatabase localDatabase)
    {
        _localDatabase = localDatabase;
    }

    public async Task Insert(Friend friend, CancellationToken cancellationToken)
    {
        const string sql = @"insert into friends () ";

        var command = new CommandDefinition(sql, friend, transaction: _localDatabase.Transaction, cancellationToken: cancellationToken);
        await _localDatabase.Connection.ExecuteAsync(command);
    }
}
