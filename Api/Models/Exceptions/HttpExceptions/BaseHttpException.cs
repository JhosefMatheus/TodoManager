using Api.Models.Shared;

namespace Api.Models.Exceptions.HttpExceptions;

public abstract class BaseHttpException : BaseException
{
    public int Status { get; set; }

    public BaseHttpException(
        string baseMessage,
        Exception baseInnerException,
        AlertVariant variant,
        int status
    ) : base(baseMessage, baseInnerException, variant)
    {
        Status = status;
    }

    public BaseHttpException(string baseMessage, AlertVariant variant, int status) : base(baseMessage, variant)
    {
        Status = status;
    }
}