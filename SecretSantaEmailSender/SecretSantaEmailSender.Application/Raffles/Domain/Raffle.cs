namespace SecretSantaEmailSender.Application.Raffles.Domain;

public class Raffle
{
    public long ID { get; private set; }
    public long SecretSantaID { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Raffle(long iD, long secretSantaID, DateTime createdAt)
    {
        ID = iD;
        SecretSantaID = secretSantaID;
        CreatedAt = createdAt;
    }

    public static Raffle Create(long secretSantaID) => new(0, secretSantaID, DateTime.Now);
}
