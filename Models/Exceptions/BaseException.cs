using TodoManager.Models.Shared;

namespace TodoManager.Models.Exceptions;

public abstract class BaseException : Exception
{
    public string BaseMessage { get; set; }
    public string? ErrorMessage { get; set; }
    public Exception? BaseInnerException { get; set; }
    public AlertVariant Variant { get; set; }

    public BaseException(
        string baseMessage,
        string errorMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(errorMessage, baseInnerException)
    {
        BaseMessage = baseMessage;
        ErrorMessage = errorMessage;
        BaseInnerException = baseInnerException;
        Variant = variant;
    }

    public BaseException(string baseMessage, AlertVariant variant) : base()
    {
        BaseMessage = baseMessage;
        Variant = variant;
    }
}