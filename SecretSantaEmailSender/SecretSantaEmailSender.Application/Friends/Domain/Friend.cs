namespace SecretSantaEmailSender.Application.Friends.Domain;

public class Friend
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DestinationLink { get; set; }

    public Friend(long iD, string name, string email, string presentSuggestions)
    {
        ID = iD;
        Name = name;
        Email = email;
        DestinationLink = presentSuggestions;
    }
}
