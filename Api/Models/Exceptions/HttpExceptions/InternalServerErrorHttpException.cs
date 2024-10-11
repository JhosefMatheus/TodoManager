using Api.Models.Shared;

namespace Api.Models.Exceptions.HttpExceptions;

public class InternalServerErrorHttpException : BaseHttpException
{
    public InternalServerErrorHttpException(
        string baseMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseMessage, baseInnerException, variant, 500) { }

    public InternalServerErrorHttpException(
        string baseMessage,
        AlertVariant variant
    ) : base(baseMessage, variant, 500) { }
}