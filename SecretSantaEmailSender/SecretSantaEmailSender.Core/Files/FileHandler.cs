using SecretSantaEmailSender.Core.Results;

namespace SecretSantaEmailSender.Core.Files;

public static class FileHandler
{
    public static Result<string> GetFileContent(string filePath)
    {
        if (!File.Exists(filePath))
            return Result.Failure<string>("Arquivo inexistente.");

        return File.ReadAllText(filePath);
    }
}
