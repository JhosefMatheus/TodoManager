using Api.Models.Shared;

namespace Api.Models.Exceptions.HttpExceptions;

public class BadHttpException : BaseHttpException
{
    public BadHttpException(
        string baseMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseMessage, baseInnerException, variant, 400) { }

    public BadHttpException(
        string baseMessage,
        AlertVariant variant
    ) : base(baseMessage, variant, 400) { }
}