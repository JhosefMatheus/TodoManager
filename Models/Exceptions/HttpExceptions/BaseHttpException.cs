using TodoManager.Models.Shared;

namespace TodoManager.Models.Exceptions.HttpExceptions;

public abstract class BaseHttpException : BaseException
{
    public int Status { get; set; }

    public BaseHttpException(
        string baseMessage,
        string errorMessage,
        Exception baseInnerException,
        AlertVariant variant,
        int status
    ) : base(baseMessage, errorMessage, baseInnerException, variant)
    {
        Status = status;
    }

    public BaseHttpException(string baseMessage, AlertVariant variant, int status) : base(baseMessage, variant)
    {
        Status = status;
    }
}