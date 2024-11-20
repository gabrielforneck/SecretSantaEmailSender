using SecretSantaEmailSender.Application.Emails.Model.Enums;

namespace SecretSantaEmailSender.Application.SecretFriends.Models.DTOs;

public class EmailDataDto
{
    public string SecretSantaName { get; set; } = string.Empty;
    public string EmailDesign { get; set; } = string.Empty;
    public EEmailDesignType EmailDesignType { get; set; }
    public string LinkPlaceholder { get; set; } = string.Empty;
    public string FriendEmail { get; set; } = string.Empty;
    public string SecretFriendUrl { get; set; } = string.Empty;
}
