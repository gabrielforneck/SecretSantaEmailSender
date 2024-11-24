using MediatR;
using SecretSantaEmailSender.Application.Friends.Repository;
using SecretSantaEmailSender.Application.Raffles.Repository;
using SecretSantaEmailSender.Application.SecretFriends.Repository;
using SecretSantaEmailSender.Application.SecretSantas.Commands;
using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Application.SecretSantas.Repository;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Handlers;

public class SecretSantasHandler : IRequestHandler<CreateSecretSantaCommand, Result>, IRequestHandler<UpdateSecretSantaCommand, Result>, IRequestHandler<DeleteSecretSantaCommand, Result>
{
    private readonly ISecretSantaRepository _secretSantaRepository;
    private readonly ISecretFriendRepository _secretFriendRepository;
    private readonly IRaffleRepository _raffleRepository;
    private readonly IFriendRepository _friendRepository;

    public SecretSantasHandler(ISecretSantaRepository secretSantaRepository, ISecretFriendRepository secretFriendRepository, IRaffleRepository raffleRepository, IFriendRepository friendRepository)
    {
        _secretSantaRepository = secretSantaRepository;
        _secretFriendRepository = secretFriendRepository;
        _raffleRepository = raffleRepository;
        _friendRepository = friendRepository;
    }

    public async Task<Result> Handle(CreateSecretSantaCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFailure)
            return requestValidation;

        var secretSanta = SecretSanta.Create(request.Icon, request.Name, request.EmailDesignType, request.EmailDesign, request.LinkPlaceholder);

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretSantaRepository.Insert(secretSanta, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(UpdateSecretSantaCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFailure)
            return requestValidation;

        var secretSanta = await _secretSantaRepository.GetByID(request.ID, cancellationToken);
        if (secretSanta == null)
            return Result.Failure("Amigo secreto não encontrado.");

        secretSanta.Update(request.Icon, request.Name, request.EmailDesignType, request.EmailDesign, request.LinkPlaceholder);

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretSantaRepository.Update(secretSanta, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> Handle(DeleteSecretSantaCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validate();
        if (validationResult.IsFailure)
            return validationResult;

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretFriendRepository.DeleteBySecretSantaID(request.ID, cancellationToken);

        await _raffleRepository.DeleteBySecretSantaID(request.ID, cancellationToken);

        await _friendRepository.DeleteBySecretSantaID(request.ID, cancellationToken);

        await _secretSantaRepository.Delete(request.ID, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
