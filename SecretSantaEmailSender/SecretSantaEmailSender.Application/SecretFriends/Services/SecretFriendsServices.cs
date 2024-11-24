using SecretSantaEmailSender.Application.Friends.Domain;
using SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;
using SecretSantaEmailSender.Core.Lists.Extensions;
using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Application.SecretFriends.Services;

public class SecretFriendsServices : ISecretFriendsServices
{
    public Result<IList<SecretFriendDto>> GenerateSecretFriends(IEnumerable<Friend> friends, Random? random = null)
    {
        if (friends.Count() < 2)
            return Result.Failure<IList<SecretFriendDto>>("São necessários pelo menos dois amigos para realizar o sorteio.");

        random ??= new Random();
        var friendsList = friends.ToList();
        friendsList.Shuffle();
        var isEven = friendsList.Count % 2 == 0;

        if (isEven)
            return GenerateEvenSecretFriends(friendsList, random: random);

        var quantityToGenerateEven = random.Next(0, (friendsList.Count - 2) / 2) * 2;

        var friendsToGenerateEven = new List<Friend>();
        for (var i = 0; i < quantityToGenerateEven; i++)
        {
            var selectedIndex = random.Next(0, friendsList.Count);
            friendsToGenerateEven.Add(friendsList[selectedIndex]);
            friendsList.RemoveAt(selectedIndex);
        }

        var generatedSecretFriends = new List<SecretFriendDto>();

        if (friendsToGenerateEven.Count > 0)
        {
            var generateEvenResult = GenerateEvenSecretFriends(friendsToGenerateEven, random: random);
            if (generateEvenResult.IsFailure)
                return generateEvenResult;

            generatedSecretFriends.AddRange(generateEvenResult.Value!);
        }

        var genereateOddResult = GenerateOddSecretFriends(friendsList, random: random);
        if (genereateOddResult.IsFailure)
            return genereateOddResult;

        generatedSecretFriends.AddRange(genereateOddResult.Value!);

        return generatedSecretFriends;
    }

    private Result<IList<SecretFriendDto>> GenerateEvenSecretFriends(List<Friend> friends, Random? random = null)
    {
        if (friends.Count < 2)
            return Result.Failure<IList<SecretFriendDto>>("São necessários pelo menos dois amigos para realizar o sorteio.");

        if (friends.Count % 2 != 0)
            return Result.Failure<IList<SecretFriendDto>>("A lista informada não tem uma quantidade par de elementos.");

        random ??= new Random();

        friends.Shuffle(random: random);

        var secretFriends = new List<SecretFriendDto>();
        var alreadySortedFriends = new List<Friend>();

        foreach (var friend in friends)
        {
            var eligibleSecretFriends = friends.Except(alreadySortedFriends).Except([friend]).ToList();

            var selectedIndex = random.Next(0, eligibleSecretFriends.Count);

            secretFriends.Add(new(friend.ID, eligibleSecretFriends[selectedIndex].ID));
            alreadySortedFriends.Add(eligibleSecretFriends[selectedIndex]);
        }

        return secretFriends;
    }

    private Result<IList<SecretFriendDto>> GenerateOddSecretFriends(List<Friend> friends, Random? random = null)
    {
        if (friends.Count < 3)
            return Result.Failure<IList<SecretFriendDto>>("São necessários pelo menos três amigos para realizar o sorteio.");

        if (friends.Count % 2 != 1)
            return Result.Failure<IList<SecretFriendDto>>("A lista informada não tem uma quantidade ímpar de elementos.");

        random ??= new Random();

        friends.Shuffle(random: random);

        var secretFriends = new List<SecretFriendDto>();
        var alreadySortedFriends = new List<Friend>();

        var firstFriend = friends[random.Next(0, friends.Count)];
        var currentFriend = firstFriend;

        while (true)
        {
            var eligibleSecretFriends = friends.Except(alreadySortedFriends).Except([currentFriend, firstFriend]).ToList();
            if (eligibleSecretFriends.Count == 0)
                break;

            var selectedIndex = random.Next(0, eligibleSecretFriends.Count);

            secretFriends.Add(new(currentFriend.ID, eligibleSecretFriends[selectedIndex].ID));

            currentFriend = eligibleSecretFriends[selectedIndex];
            alreadySortedFriends.Add(currentFriend);
        }

        secretFriends.Add(new(currentFriend.ID, firstFriend.ID));

        return secretFriends;
    }
}
