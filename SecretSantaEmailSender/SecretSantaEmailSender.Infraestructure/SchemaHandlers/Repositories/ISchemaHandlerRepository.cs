using SecretSantaEmailSender.Core.Database.Respositories.Interfaces;

namespace SecretSantaEmailSender.Infraestructure.SchemaHandlers.Repositories;

public interface ISchemaHandlerRepository : IRepository
{
    Task CreateFriendsTable();
    Task CreateRafflesTable();
    Task CreateSecretFriendsRafflesAndFriendsUniqueIndex();
    Task CreateSecretFriendsRafflesAndSecretFriendsUniqueIndex();
    Task CreateSecretFriendsTable();
    Task CreateSecretSantasTable();
}