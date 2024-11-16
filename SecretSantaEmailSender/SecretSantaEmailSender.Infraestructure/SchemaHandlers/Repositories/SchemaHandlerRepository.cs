using Dapper;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Infraestructure.ScheaHandlers.Repositories;

public class SchemaHandlerRepository : ISchemaHandlerRepository
{
    public ILocalDatabase LocalDatabase { get; }

    public SchemaHandlerRepository(ILocalDatabase localDatabase)
    {
        LocalDatabase = localDatabase;
    }

    public async Task SetEncoding(string encoding)
    {
        const string sql = @"PRAGMA encoding = @encoding";

        var commandDefinition = new CommandDefinition(sql, new { encoding }, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(commandDefinition);
    }

    public async Task CreateSecretSantasTable()
    {
        const string sql = @"create table if not exists secret_santas (
                                 id integer not null primary key autoincrement,
                                 icon text,
                                 name text not null,
                                 created_at integer not null,
                                 email_design_type integer not null check (email_design_type in (1, 2)),
                                 email_design text not null,
                                 link_placeholder text not null)";

        var command = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task CreateFriendsTable()
    {
        const string sql = @"create table if not exists friends (
                                 id integer not null primary key autoincrement,
                                 secret_santa_id integer not null references secret_santas (id),
                                 name text not null,
                                 email text not null,
                                 destination_url not null)";

        var command = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task CreateRafflesTable()
    {
        const string sql = @"create table if not exists raffles (
                                 id integer not null primary key autoincrement,
                                 secret_santa_id integer not null references secret_santas (id),
                                 created_at integer not null)";

        var command = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task CreateSecretFriendsTable()
    {
        const string sql = @"create table if not exists secret_friends (
                                 id integer not null primary key autoincrement,
                                 raffle_id integer not null references raffles (id),
                                 friend_id integer not null references friends (id),
                                 secret_friend_id integer not null references friends (id),
                                 email_sent_at integer)";

        var comand = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(comand);
    }

    public async Task CreateSecretFriendsRafflesAndFriendsUniqueIndex()
    {
        const string sql = @"create unique index if not exists secret_friends_01_idx on secret_friends (raffle_id, friend_id)";

        var command = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task CreateSecretFriendsRafflesAndSecretFriendsUniqueIndex()
    {
        const string sql = @"create unique index if not exists secret_friends_02_idx on secret_friends (raffle_id, secret_friend_id)";

        var command = new CommandDefinition(sql, transaction: LocalDatabase.Transaction);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}