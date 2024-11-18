using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Files.Commands;

public class GetFileContentCommand : IRequest<Result<string>>
{
    public string FilePath { get; set; }

    public GetFileContentCommand(string filePath)
    {
        FilePath = filePath;
    }

    public Result Validate()
    {
        if (string.IsNullOrWhiteSpace(FilePath))
            return Result.Failure("Arquivo inválido.");

        return Result.Success();
    }
}
