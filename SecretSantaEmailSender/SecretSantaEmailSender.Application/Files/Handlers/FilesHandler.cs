using MediatR;
using SecretSantaEmailSender.Application.Files.Commands;
using SecretSantaEmailSender.Core.Files;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Files.Handlers;

public class FilesHandler : IRequestHandler<GetFileContentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(GetFileContentCommand request, CancellationToken cancellationToken)
    {
        var requestValidation = request.Validate();
        if (requestValidation.IsFailure)
            return await Task.FromResult(Result.Failure<string>(requestValidation.Message));
        
        return await Task.FromResult(FileHandler.GetFileContent(request.FilePath));
    }
}
