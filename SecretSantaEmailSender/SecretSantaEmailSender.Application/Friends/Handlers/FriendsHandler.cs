using MediatR;
using SecretSantaEmailSender.Application.Friends.Commands;
using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Application.Friends.Repository;
using SecretSantaEmailSender.Application.SecretFriends.Repository;
using SecretSantaEmailSender.Application.SecretSantas.Queries;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Friends.Handlers;

public class FriendsHandler : IRequestHandler<AddFriendCommand, Result>, IRequestHandler<UpdateFriendCommand, Result>, IRequestHandler<DeleteFriendCommand, Result>
{
    private readonly IFriendRepository _friendRepository;
    private readonly ISecretFriendRepository _secretFriendRepository;
    private readonly ISecretSantasQueries _secretSantasQueries;

    public FriendsHandler(IFriendRepository friendRepository, ISecretFriendRepository secretFriendRepository, ISecretSantasQueries secretSantasQueries)
    {
        _friendRepository = friendRepository;
        _secretFriendRepository = secretFriendRepository;
        _secretSantasQueries = secretSantasQueries;
    }

    public async Task<Result> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFailure)
            return requestValidation;

        if (!await _secretSantasQueries.SecretSantaExists(request.SecretSantaID, cancellationToken))
            return Result.Failure("Amigo secreto não encontrado.");

        var newFriend = Friend.Create(request.SecretSantaID, request.Name, request.Email, request.DestinationLink);

        _friendRepository.LocalDatabase.Begin();

        await _friendRepository.Insert(newFriend, cancellationToken);

        await _friendRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFailure)
            return requestValidation;

        var friend = await _friendRepository.GetByID(request.ID, cancellationToken);
        if (friend == null)
            return Result.Failure("Amigo não encontrado.");

        friend.Update(friend.SecretSantaID, request.Name, request.Email, request.DestinationLink);

        _friendRepository.LocalDatabase.Begin();

        await _friendRepository.Update(friend, cancellationToken);

        await _friendRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validate();
        if (validationResult.IsFailure)
            return validationResult;

        _friendRepository.LocalDatabase.Begin();

        await _secretFriendRepository.DeleteByFriendID(request.ID, cancellationToken);
        await _secretFriendRepository.DeleteBySecretFriendID(request.ID, cancellationToken);

        await _friendRepository.Delete(request.ID, cancellationToken);

        await _friendRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
