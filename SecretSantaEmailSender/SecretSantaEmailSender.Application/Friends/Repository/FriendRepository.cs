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

    public async Task Insert(Friend friend, CancellationToken cancellationToken)
    {
        const string sql = @"insert into friends (secret_santa_id,
                                                  name,
                                                  email,
                                                  destination_link)
                                          values (@Name,
                                                  @Email,
                                                  @DestinationLink)";

        var command = new CommandDefinition(sql, friend, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
