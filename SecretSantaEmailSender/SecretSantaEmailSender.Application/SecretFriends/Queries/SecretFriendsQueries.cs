using Dapper;
using SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.SecretFriends.Queries;

public class SecretFriendsQueries : ISecretFriendsQueries
{
    private readonly ILocalDatabase _localDatabase;

    public SecretFriendsQueries(ILocalDatabase localDatabase)
    {
        _localDatabase = localDatabase;
    }

    public async Task<EmailDataDto?> GetEmailData(long secretFriendID, CancellationToken cancellationToken)
    {
        const string sql = @"select secret_santas.name as SecretSantaName,
                                    secret_santas.email_design as EmailDesign,
                                    secret_santas.email_design_type as EmailDesignType,
                                    secret_santas.link_placeholder as LinkPlaceholder,
                                    friend.email as FriendEmail,
                                    secret_friend.destination_url as SecretFriendUrl
                               from secret_friends
                              inner join raffles
                                 on raffles.id = secret_friends.raffle_id
                              inner join secret_santas
                                 on secret_santas.id = raffles.secret_santa_id
                              inner join friends as friend
                                 on secret_friends.friend_id = friend.id
                              inner join friends as secret_friend
                                 on secret_friends.secret_friend_id = secret_friend.id
                              where secret_friends.id = @secretFriendID";

        var command = new CommandDefinition(sql, new { secretFriendID }, transaction: _localDatabase.Transaction, cancellationToken: cancellationToken);
        return await _localDatabase.Connection.QuerySingleOrDefaultAsync<EmailDataDto>(command);
    }

    public async Task<IEnumerable<long>> GetSecretFriendsIdsByRaffleID(long raffleID, CancellationToken cancellationToken)
    {
        const string sql = @"select secret_friends.id
                               from secret_friends
                              where secret_friends.raffle_id = @raffleID";

        var command = new CommandDefinition(sql, new { raffleID }, transaction: _localDatabase.Transaction, cancellationToken: cancellationToken);
        return await _localDatabase.Connection.QueryAsync<long>(command);
    }
}
