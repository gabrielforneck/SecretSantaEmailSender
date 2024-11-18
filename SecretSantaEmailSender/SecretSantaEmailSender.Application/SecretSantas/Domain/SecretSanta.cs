using SecretSantaEmailSender.Application.Email.Model.Enums;

namespace SecretSantaEmailSender.Application.SecretSantas.Domain;

public class SecretSanta
{
    public long ID { get; private set; }
    public string? Icon { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public EEmailDesignType EmailDesignType { get; private set; }
    public string EmailDesign { get; private set; }
    public string LinkPlaceholder { get; private set; }

    private SecretSanta(long iD, string? icon, string name, DateTime createdAt, EEmailDesignType emailDesignType, string emailDesign, string linkPlaceholder)
    {
        ID = iD;
        Icon = icon;
        Name = name;
        CreatedAt = createdAt;
        EmailDesignType = emailDesignType;
        EmailDesign = emailDesign;
        LinkPlaceholder = linkPlaceholder;
    }

    public static SecretSanta Create(string? icon, string name, EEmailDesignType emailDesignType, string emailDesign, string linkPlaceholder) => new(0, icon, name, DateTime.Now, emailDesignType, emailDesign, linkPlaceholder);

    public void Update(string? icon, string name, EEmailDesignType emailDesignType, string emailDesign, string linkPlaceholder)
    {
        Icon = icon;
        Name = name;
        EmailDesignType = emailDesignType;
        EmailDesign = emailDesign;
        LinkPlaceholder = linkPlaceholder;
    }
}
