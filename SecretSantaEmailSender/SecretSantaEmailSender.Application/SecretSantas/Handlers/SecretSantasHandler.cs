using MediatR;
using SecretSantaEmailSender.Application.SecretSantas.Commands;
using SecretSantaEmailSender.Application.SecretSantas.Domain;
using SecretSantaEmailSender.Application.SecretSantas.Repository;
using SecretSantaEmailSender.Core.Files;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Handlers;

public class SecretSantasHandler : IRequestHandler<CreateSecretSantaCommand, Result>
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

        var fileContentResult = FileHandler.GetFileContent(request.EmailDesignPath);
        if (fileContentResult.IsFilure)
            return fileContentResult;

        var secretSanta = SecretSanta.Create(request.Icon, request.Name, request.EmailDesignType, fileContentResult.Value!, request.LinkPlaceholder);

        _secretSantaRepository.LocalDatabase.Begin();

        await _secretSantaRepository.Insert(secretSanta, cancellationToken);

        await _secretSantaRepository.LocalDatabase.CommitAsync();

        return Result.Success();
    }
}
