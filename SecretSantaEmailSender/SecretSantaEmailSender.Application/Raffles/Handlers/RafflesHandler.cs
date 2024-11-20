using MediatR;
using SecretSantaEmailSender.Application.Database.Queries;
using SecretSantaEmailSender.Application.Friends.Repository;
using SecretSantaEmailSender.Application.Raffles.Commands;
using SecretSantaEmailSender.Application.Raffles.Domain;
using SecretSantaEmailSender.Application.Raffles.Repository;
using SecretSantaEmailSender.Application.SecretFriends.Domain;
using SecretSantaEmailSender.Application.SecretFriends.Repository;
using SecretSantaEmailSender.Application.SecretFriends.Services;
using SecretSantaEmailSender.Application.SecretSantas.Queries;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Raffles.Handlers;

public class RafflesHandler : IRequestHandler<GenerateRaffleCommand, Result>
{
    private readonly IDatabaseQueries _databaseQueries;
    private readonly IFriendRepository _friendRepository;
    private readonly ISecretSantasQueries _secretSantasQueries;
    private readonly ISecretFriendsServices _secretFriendsServices;
    private readonly IRaffleRepository _raffleRepository;
    private readonly ISecretFriendRepository _secretFriendRepository;

    public RafflesHandler(IDatabaseQueries databaseQueries, IFriendRepository friendRepository, ISecretSantasQueries secretSantasQueries, ISecretFriendsServices secretFriendsServices, IRaffleRepository raffleRepository, ISecretFriendRepository secretFriendRepository)
    {
        _databaseQueries = databaseQueries;
        _friendRepository = friendRepository;
        _secretSantasQueries = secretSantasQueries;
        _secretFriendsServices = secretFriendsServices;
        _raffleRepository = raffleRepository;
        _secretFriendRepository = secretFriendRepository;
    }

    public async Task<Result> Handle(GenerateRaffleCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFilure)
            return requestValidation;

        if (!await _secretSantasQueries.SecretSantaExists(request.SecretSantaID, cancellationToken))
            return Result.Failure("Amigo secreto inexistente.");

        var friends = await _friendRepository.GetBySecretSantaID(request.SecretSantaID, cancellationToken);
        
        var generateResult = _secretFriendsServices.GenerateSecretFriends(friends);
        if (generateResult.IsFilure)
            return generateResult;

        _raffleRepository.LocalDatabase.Begin();

        await _raffleRepository.Insert(Raffle.Create(request.SecretSantaID), cancellationToken);
        var raffleID = await _databaseQueries.GetLastInsertedID(cancellationToken);

        foreach (var secretFriend in generateResult.Value!)
        {
            var newSecretFriend = SecretFriend.Create(raffleID, secretFriend.FriendID, secretFriend.SecretFriendID);
            await _secretFriendRepository.Insert(newSecretFriend, cancellationToken);
        }

        await _raffleRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
