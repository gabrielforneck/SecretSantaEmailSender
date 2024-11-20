using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Services;

public class SecretFriendsServices : ISecretFriendsServices
{
    public Result<IList<SecretFriendDto>> GenerateSecretFriends(IEnumerable<Friend> friends)
    {
        if (friends.Count() < 2)
            return Result.Failure<IList<SecretFriendDto>>("São necessários pelo menos dois amigos para realizar o sorteio.");

        var secretFriends = new List<SecretFriendDto>();
        var alreadySortedFriends = new List<Friend>();

        var random = new Random();

        foreach (var friend in friends)
        {
            var ellegibleSecretFriends = friends.Except(alreadySortedFriends).Except([friend]).ToList();

            var selectedIndex = random.Next(0, ellegibleSecretFriends.Count - 1);

            secretFriends.Add(new(friend.ID, ellegibleSecretFriends[selectedIndex].ID));
            alreadySortedFriends.Add(ellegibleSecretFriends[selectedIndex]);
        }

        return secretFriends;
    }
}
