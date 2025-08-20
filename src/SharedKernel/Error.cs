namespace SharedKernel;

public class Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", ErrorType.Failure);

    public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.", ErrorType.Validation);

    public static readonly Error Unauthorized = new("Error.Unauthorized", "The current user is not authorized to perform this operation.", ErrorType.Forbidden);

    public static readonly Error Forbidden = new("Error.Forbidden", "The current user is forbidden from performing this operation.", ErrorType.Forbidden);

    public static readonly Error NotFound = new("Error.NotFound", "The requested resource was not found.", ErrorType.NotFound);

    public static readonly Error Conflict = new("Error.Conflict", "The request conflicts with the current state of the resource.", ErrorType.Conflict);

    public Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    public static implicit operator string(Error error) => error.Code;

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);
}
