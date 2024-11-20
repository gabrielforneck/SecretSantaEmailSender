using System.Data;
using System.Data.SQLite;

namespace SecretSantaEmailSender.Core.Database.Handler;

public abstract class DatabaseConnectionHandler : IDatabaseConnectionHandler
{
    public IDbConnection Connection => _connection;
    public IDbTransaction? Transaction => _transaction;
    public bool InTransaction => _inTransaction;
    public string ConnectionString => _connectionString;

    private readonly string _connectionString;
    private readonly SQLiteConnection _connection;
    private SQLiteTransaction? _transaction;
    private bool _inTransaction;

    public DatabaseConnectionHandler(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new SQLiteConnection(_connectionString);
        _connection.Open();
    }

    public void Begin()
    {
        _transaction = _connection.BeginTransaction();
        _inTransaction = true;
    }

    public void Commit()
    {
        _transaction?.Commit();
        _inTransaction = false;
    }

    public async Task CommitAsync(CancellationToken? cancellationToken = null)
    {
        await _transaction!.CommitAsync(cancellationToken ?? CancellationToken.None);

        _inTransaction = false;
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _inTransaction = false;
    }

    public async Task RollbackAsync(CancellationToken? cancellationToken = null)
    {
        await _transaction!.RollbackAsync(cancellationToken ?? CancellationToken.None);

        _inTransaction = false;
    }

    public void Dispose()
    {
        if (_inTransaction)
            _transaction?.Rollback();

        _transaction?.Dispose();
        _connection?.Dispose();

        GC.SuppressFinalize(this);
    }
}
