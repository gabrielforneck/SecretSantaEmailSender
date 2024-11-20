namespace SecretSantaEmailSender.Frontend.DependencyInjections.Exceptions;

public class BadConfigurationException : Exception
{
    public BadConfigurationException() : base() { }
    public BadConfigurationException(string? message) : base(message) { }
    public BadConfigurationException(string? message, Exception? innerException) : base(message, innerException) { }
}
