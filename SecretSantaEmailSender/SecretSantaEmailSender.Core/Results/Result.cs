namespace SecretSantaEmailSender.Core.Results;

public class Result
{
    protected readonly bool _isSuccess;
    protected readonly string _message;

    internal Result(bool isSuccess, string message)
    {
        _isSuccess = isSuccess;
        _message = message;
    }

    public static Result Success() => new(true, "");
    public static Result Failure(string message) => new(false, message);

    public static Result<T> Success<T>(T value) => new(true, "", value);

    public static Result<T> Failure<T>(string message) => new(false, message, default);

    public bool IsSuccess => _isSuccess;
    public bool IsFailure => !_isSuccess;
    public string Message => _message;
}

public class Result<T> : Result
{
    protected readonly T? _value;

    internal Result(bool isSuccess, string message, T? value) : base(isSuccess, message)
    {
        _value = value;
    }

    public T? Value => _value;

    public static implicit operator Result<T>(T value) => Success(value);
}
