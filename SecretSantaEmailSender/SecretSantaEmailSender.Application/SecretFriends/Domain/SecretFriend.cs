namespace SecretSantaEmailSender.Application.SecretFriends.Domain;

public class SecretFriend
{
    public long ID { get; private set; }
    public long RaffleID { get; private set; }
    public long FriendID { get; private set; }
    public long SecretFriendID { get; private set; }
    public DateTime? EmailSentAt { get; private set; }

    private SecretFriend(long iD, long raffleID, long friendID, long secretFriendID, DateTime? emailSentAt)
    {
        ID = iD;
        RaffleID = raffleID;
        FriendID = friendID;
        SecretFriendID = secretFriendID;
        EmailSentAt = emailSentAt;
    }

    public static SecretFriend Create(long raffleID, long friendID, long secretFriendID) => new(0, raffleID, friendID, secretFriendID, null);
}
