using TodoManager.Models.Shared;

namespace TodoManager.Models.Exceptions.HttpExceptions;

public class BadHttpException : BaseHttpException
{
    public BadHttpException(
        string baseMessage,
        string errorMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseMessage, errorMessage, baseInnerException, variant, 400) { }

    public BadHttpException(
        string baseMessage,
        AlertVariant variant
    ) : base(baseMessage, variant, 400) { }
}