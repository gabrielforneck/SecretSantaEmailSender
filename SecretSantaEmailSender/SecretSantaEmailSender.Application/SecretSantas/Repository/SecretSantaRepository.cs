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
}
