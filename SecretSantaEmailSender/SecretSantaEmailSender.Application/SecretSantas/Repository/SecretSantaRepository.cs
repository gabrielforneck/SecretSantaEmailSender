using Dapper;
using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Core.Database;

namespace SecretSantaEmailSender.Application.SecretSantas.Repository;

public class SecretSantaRepository : ISecretSantaRepository
{
    public ILocalDatabase LocalDatabase { get; }

    public SecretSantaRepository(ILocalDatabase localDatabase)
    {
        LocalDatabase = localDatabase;
    }

    public async Task<SecretSanta?> GetByID(long ID, CancellationToken cancellationToken)
    {
        const string sql = @"select id as ID,
                                    icon as Icon,
                                    name as Name,
                                    created_at as CreatedAt,
                                    email_design_type as EmailDesignType,
                                    email_design as EmailDesign,
                                    link_placeholder as LinkPlaceholder
                               from secret_santas
                              where id = @ID";

        var command = new CommandDefinition(sql, new { ID }, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        return await LocalDatabase.Connection.QuerySingleOrDefaultAsync<SecretSanta>(command);
    }

    public async Task Insert(SecretSanta secretSanta, CancellationToken cancellationToken)
    {
        const string sql = @"insert into secret_santas (icon,
                                                        name,
                                                        created_at,
                                                        email_design_type,
                                                        email_design,
                                                        link_placeholder)
                                                values (@Icon,
                                                        @Name,
                                                        @CreatedAt,
                                                        @EmailDesignType,
                                                        @EmailDesign,
                                                        @LinkPlaceholder)";

        var command = new CommandDefinition(sql, secretSanta, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }

    public async Task Update(SecretSanta secretSanta, CancellationToken cancellationToken)
    {
        const string sql = @"update secret_santas
                                set icon = @Icon,
                                    name = @Name,
                                    created_at = @CreatedAt,
                                    email_design_type = @EmailDesignType,
                                    email_design = @EmailDesign,
                                    link_placeholder = @LinkPlaceholder
                              where id = @ID";

        var command = new CommandDefinition(sql, secretSanta, transaction: LocalDatabase.Transaction, cancellationToken: cancellationToken);
        await LocalDatabase.Connection.ExecuteAsync(command);
    }
}
