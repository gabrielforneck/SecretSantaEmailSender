using MediatR;
using SecretSantaEmailSender.Application.SecretSantas.Commands;
using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Application.SecretSantas.Repository;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Handlers;

public class SecretSantasHandler : IRequestHandler<CreateSecretSantaCommand, Result>, IRequestHandler<UpdateSecretSantaCommand, Result>
{
    private readonly ISecretSantaRepository _secretSantaRepository;

    public SecretSantasHandler(ISecretSantaRepository secretSantaRepository)
    {
        _secretSantaRepository = secretSantaRepository;
    }

    public async Task<Result> Handle(CreateSecretSantaCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFilure)
            return requestValidation;

        var secretSanta = SecretSanta.Create(request.Icon, request.Name, request.EmailDesignType, request.EmailDesign, request.LinkPlaceholder);

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretSantaRepository.Insert(secretSanta, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync();

        return Result.Success();
    }

    public async Task<Result> Handle(UpdateSecretSantaCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFilure)
            return requestValidation;

        var secretSanta = await _secretSantaRepository.GetByID(request.ID, cancellationToken);
        if (secretSanta == null)
            return Result.Failure("Amigo secreto não encontrado.");

        secretSanta.Update(request.Icon, request.Name, request.EmailDesignType, request.EmailDesign, request.LinkPlaceholder);

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretSantaRepository.Update(secretSanta, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync();

        return Result.Success();
    }

    //TODO: Criar delete quando todos os repositories tiverem sido feitos
}
