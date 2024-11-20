﻿using SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;

namespace SecretSantaEmailSender.Application.SecretFriends.Queries;

public interface ISecretFriendsQueries
{
    Task<EmailDataDto?> GetEmailData(long secretFriendID, CancellationToken cancellationToken);
}