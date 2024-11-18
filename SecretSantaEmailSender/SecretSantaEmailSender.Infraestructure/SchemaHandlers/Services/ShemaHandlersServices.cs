﻿using SecretSantaEmailSender.Infraestructure.ScheaHandlers.Repositories;

namespace SecretSantaEmailSender.Infraestructure.ScheaHandlers.Services;

public class ShemaHandlersServices : IShemaHandlersServices
{
    private readonly ISchemaHandlerRepository _schemaHandlerRepository;

    public ShemaHandlersServices(ISchemaHandlerRepository schemaHandlerRepository)
    {
        _schemaHandlerRepository = schemaHandlerRepository;
    }

    public async Task CreateSchema()
    {
        _schemaHandlerRepository.LocalDatabase.Begin();

        //TODO: Por em arquivo config
        await _schemaHandlerRepository.SetEncoding("UTF-16");

        await _schemaHandlerRepository.CreateSecretSantasTable();

        await _schemaHandlerRepository.CreateFriendsTable();

        await _schemaHandlerRepository.CreateRafflesTable();

        await _schemaHandlerRepository.CreateSecretFriendsTable();

        await _schemaHandlerRepository.CreateSecretFriendsRafflesAndFriendsUniqueIndex();

        await _schemaHandlerRepository.CreateSecretFriendsRafflesAndSecretFriendsUniqueIndex();

        await _schemaHandlerRepository.LocalDatabase.CommitAsync();
    }
}