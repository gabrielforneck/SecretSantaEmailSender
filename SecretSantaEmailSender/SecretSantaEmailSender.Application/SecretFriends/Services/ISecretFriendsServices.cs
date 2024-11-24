using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Services;

public interface ISecretFriendsServices
{
    Result<IList<SecretFriendDto>> GenerateSecretFriends(IEnumerable<Friend> friends, Random? random = null);
}