using TodoManager.Models.Shared;

namespace TodoManager.Models.Exceptions.HttpExceptions;

public abstract class BaseHttpException : BaseException
{
    public BaseHttpException(
        string baseMessage,
        string errorMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseMessage, errorMessage, baseInnerException, variant) { }

    public BaseHttpException(string baseMessage, AlertVariant variant) : base(baseMessage, variant) { }
}