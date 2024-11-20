using Dapper;
using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.Friends.Repository;

public class FriendRepository : IFriendRepository
{
    public ILocalDatabase LocalDatabase { get; }

    public FriendRepository(ILocalDatabase localDatabase)
    {
        LocalDatabase = localDatabase;
    }

    public async Task<Friend?> GetByID(long iD, CancellationToken cancellationToken)
    {
        const string sql = @"select id as ID,
                                    secret_santa_id as SecretSantaID,
                                    name as Name,
                                    email as Email,
                                    destination_link as DestinationLink
                               from friends
                              where id = @iD";

        var command = new CommandDefinition(sql, new { iD }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        return await LocalDatabase.Connection.QuerySingleOrDefaultAsync<Friend>(command);
    }

    public async Task<IEnumerable<Friend>> GetBySecretSantaID(long secretSantaID, CancellationToken cancellationToken)
    {
        const string sql = @"select friends.id as ID,
                                    friends.secret_santa_id as SecretSantaID,
                                    friends.name as Name,
                                    friends.email as Email,
                                    friends.destination_link as DestinationLink
                               from friends
                              where friends.secret_santa_id = @secretSantaID";

        var command = new CommandDefinition(sql, new { secretSantaID }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        return await LocalDatabase.Connection.QueryAsync<Friend>(command);
    }

    public async Task Insert(Friend friend, CancellationToken cancellationToken)
    {
        const string sql = @"insert into friends (secret_santa_id,
                                                  name,
                                                  email,
                                                  destination_link)
                                          values (@SecretSantaID,
                                                  @Name,
                                                  @Email,
                                                  @DestinationLink)";

        var command = new CommandDefinition(sql, friend, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Update(Friend friend, CancellationToken cancellationToken)
    {
        const string sql = @"update friends
                                set secret_santa_id = @SecretSantaID,
                                    name = @Name,
                                    email = @Email,
                                    destination_link = @DestinationLink)
                              where id = @ID";

        var command = new CommandDefinition(sql, friend, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Delete(long iD, CancellationToken cancellationToken)
    {
        const string sql = @"delete from friends
                              where id = @iD";

        var command = new CommandDefinition(sql, new { iD }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task DeleteBySecretSantaID(long secretSantaID, CancellationToken cancellationToken)
    {
        const string sql = @"delete from friends
                              where secret_santa_id = @secretSantaID";

        var command = new CommandDefinition(sql, new { secretSantaID }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
