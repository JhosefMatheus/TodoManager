using Api.Models.Shared;

namespace Api.Models.Exceptions;

public abstract class BaseException : Exception
{
    public string BaseMessage { get; set; }
    public string? ErrorMessage { get; set; }
    public Exception? BaseInnerException { get; set; }
    public AlertVariant Variant { get; set; }

    public BaseException(
        string baseMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseInnerException.Message, baseInnerException)
    {
        BaseMessage = baseMessage;
        ErrorMessage = baseInnerException.Message;
        BaseInnerException = baseInnerException;
        Variant = variant;
    }

    public BaseException(string baseMessage, AlertVariant variant) : base()
    {
        BaseMessage = baseMessage;
        Variant = variant;
    }
}