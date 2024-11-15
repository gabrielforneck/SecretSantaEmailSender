namespace SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

public interface IRepository
{
    ILocalDatabase LocalDatabase { get; }
}
