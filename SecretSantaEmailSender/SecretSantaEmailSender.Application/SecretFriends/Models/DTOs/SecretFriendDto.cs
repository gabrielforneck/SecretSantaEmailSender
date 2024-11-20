namespace SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;

public class SecretFriendDto
{
    public long FriendID { get; set; }
    public long SecretFriendID { get; set; }

    public SecretFriendDto(long friendID, long secretFriendID)
    {
        FriendID = friendID;
        SecretFriendID = secretFriendID;
    }
}
