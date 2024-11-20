using Dapper;
using SecretSantaEmailSender.Application.SecretFriends.Domain;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.SecretFriends.Repository;

public class SecretFriendRepository : ISecretFriendRepository
{
    public ILocalDatabase LocalDatabase { get; }

    public SecretFriendRepository(ILocalDatabase localDatabase)
    {
        LocalDatabase = localDatabase;
    }

    public async Task Insert(SecretFriend secretFriend, CancellationToken cancellationToken)
    {
        const string sql = @"insert into secret_friends (raffle_id,
                                                         friend_id,
                                                         secret_friend_id,
                                                         email_sent_at)
                                                 values (@RaffleID,
                                                         @FriendID
                                                         @SecretFriendID
                                                         @EmailSentAt)";

        var command = new CommandDefinition(sql, secretFriend, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task DeleteByFriendID(long friendID, CancellationToken cancellationToken)
    {
        const string sql = @"delete from secret_friends
                              where friend_id = @friendID";

        var command = new CommandDefinition(sql, new { friendID }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task DeleteBySecretFriendID(long secretFriendID, CancellationToken cancellationToken)
    {
        const string sql = @"delete from secret_friends
                              where secret_friend_id = @secretFriendID";

        var command = new CommandDefinition(sql, new { secretFriendID }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
