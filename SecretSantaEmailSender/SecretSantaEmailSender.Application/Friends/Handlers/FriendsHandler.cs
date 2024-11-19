using MediatR;
using SecretSantaEmailSender.Application.Friends.Commands;
using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Application.Friends.Repository;
using SecretSantaEmailSender.Application.SecretSantas.Repository;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Friends.Handlers;

public class FriendsHandler : IRequestHandler<AddFriendCommand, Result>
{
    private readonly ISecretSantaRepository _secretSantaRepository;
    private readonly IFriendRepository _friendRepository;

    public FriendsHandler(ISecretSantaRepository secretSantaRepository, IFriendRepository friendRepository)
    {
        _secretSantaRepository = secretSantaRepository;
        _friendRepository = friendRepository;
    }

    public async Task<Result> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFilure)
            return requestValidation;

        var secretSanta = await _secretSantaRepository.GetByID(request.SecretSantaID, cancellationToken);
        if (secretSanta == null)
            return Result.Failure("Amigo secreto não encontrado.");

        var newFriend = Friend.Create(request.Name, request.Email, request.DestinationLink);

        _friendRepository.LocalDatabase.Begin();

        await _friendRepository.Insert(newFriend, cancellationToken);

        await _friendRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
