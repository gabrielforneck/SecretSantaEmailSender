using System.Data;

namespace SecretSantaEmailSender.Core.Database.Handler;

public interface IDatabaseConnectionHandler
{
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }
    bool InTransaction { get; }
    string ConnectionString { get; }

    void Begin();
    void Commit();
    Task CommitAsync(CancellationToken? cancellationToken = null);
    void Dispose();
    void Rollback();
    Task RollbackAsync(CancellationToken? cancellationToken = null);
}