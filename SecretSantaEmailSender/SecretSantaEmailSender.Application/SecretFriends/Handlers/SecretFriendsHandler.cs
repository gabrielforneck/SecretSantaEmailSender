using MediatR;
using SecretSantaEmailSender.Application.SecretFriends.Commands;
using SecretSantaEmailSender.Application.SecretFriends.Queries;
using SecretSantaEmailSender.Application.SecretSantas.Extensions;
using SecretSantaEmailSender.Core.EmailClient.Emails;
using SecretSantaEmailSender.Core.EmailClient.Handler;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Handlers;

public class SecretFriendsHandler : IRequestHandler<SendEmailCommand, Result>, IRequestHandler<GetSecretFriendsCommand, Result<IEnumerable<long>>>
{
    private readonly ISecretFriendsQueries _secretFriendsQueries;
    private readonly IEmailClientHandler _emailClientHandler;

    public SecretFriendsHandler(ISecretFriendsQueries secretFriendsQueries, IEmailClientHandler emailClientHandler)
    {
        _secretFriendsQueries = secretFriendsQueries;
        _emailClientHandler = emailClientHandler;
    }

    public async Task<Result> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validate();
        if (validationResult.IsFailure)
            return validationResult;

        var emailData = await _secretFriendsQueries.GetEmailData(request.SecretFriendID, cancellationToken);
        if (emailData == null)
            return Result.Failure("Não foi possível carregar os dados para enviar o e-mail de amigo secreto.");

        emailData.EmailDesign = emailData.EmailDesign.Replace(emailData.LinkPlaceholder, emailData.SecretFriendLink);

        var emailBuilder = new EmailBuilder()
            .SetSubject(emailData.SecretSantaName)
            .SetBody(emailData.EmailDesign, bodyType: emailData.EmailDesignType.ToEmailBodyType())
            .AddTo(emailData.FriendEmail);

        var sendResult = await _emailClientHandler.Send(emailBuilder.Build(), cancellationToken);
        if (sendResult.IsFailure)
            return sendResult;

        return Result.Success();
    }

    public async Task<Result<IEnumerable<long>>> Handle(GetSecretFriendsCommand request, CancellationToken cancellationToken) => Result.Success(await _secretFriendsQueries.GetSecretFriendsIdsByRaffleID(request.RaffleID, cancellationToken));
}
