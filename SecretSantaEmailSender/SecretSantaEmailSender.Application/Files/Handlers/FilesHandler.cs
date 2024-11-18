using MediatR;
using SecretSantaEmailSender.Application.Files.Commands;
using SecretSantaEmailSender.Core.Files;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Files.Handlers;

public class FilesHandler : IRequestHandler<GetFileContentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(GetFileContentCommand request, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var requestValidation = request.Validate();
            if (requestValidation.IsFilure)
                return Result.Failure<string>(requestValidation.Message);

            return FileHandler.GetFileContent(request.FilePath);
        });
    }
}
