using SecretSantaEmailSender.Core.Database.Handler;

namespace SecretSantaEmailSender.Core.Database;

public sealed class LocalDatabase : DatabaseConnectionHandler, ILocalDatabase
{
    public LocalDatabase(string connectionString) : base(connectionString)
    {
    }
}
