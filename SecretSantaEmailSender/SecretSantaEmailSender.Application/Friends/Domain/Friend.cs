namespace SecretSantaEmailSender.Application.Friends.Domain;

public class Friend
{
    public long ID { get; private set; }
    public long SecretSantaID { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string DestinationLink { get; private set; }

    private Friend(long iD, long secretSantaID, string name, string email, string destinationLink)
    {
        ID = iD;
        SecretSantaID = secretSantaID;
        Name = name;
        Email = email;
        DestinationLink = destinationLink;
    }

    public static Friend Create(long secretSantaID, string name, string email, string destinationLink) => new(0, secretSantaID, name, email, destinationLink);

    public void Update(long secretSantaID, string name, string email, string destinationLink)
    {
        SecretSantaID = secretSantaID;
        Name = name;
        Email = email;
        DestinationLink = destinationLink;
    }
}
