using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretSantas.Commands;

public class DeleteSecretSantaCommand : IRequest<Result>
{
    public long ID { get; set; }

    public DeleteSecretSantaCommand(long iD)
    {
        ID = iD;
    }

    public Result Validate()
    {
        if (ID <= 0)
            return Result.Failure("ID do amigo secreto inválido.");

        return Result.Success();
    }
}
