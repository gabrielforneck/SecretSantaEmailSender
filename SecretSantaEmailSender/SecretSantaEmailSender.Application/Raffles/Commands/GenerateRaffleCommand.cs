using MediatR;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.Raffles.Commands;

public class GenerateRaffleCommand : IRequest<Result>
{
    public long SecretSantaID { get; set; }

    public GenerateRaffleCommand(long secretSantaID)
    {
        SecretSantaID = secretSantaID;
    }

    public Result Validate()
    {
        if (SecretSantaID <= 0)
            return Result.Failure("ID do amigo secreto inválido.");

        return Result.Success();
    }
}
