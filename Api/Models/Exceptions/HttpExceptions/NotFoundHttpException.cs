using Api.Models.Shared;

namespace Api.Models.Exceptions.HttpExceptions;

public class NotFoundHttpException : BaseHttpException
{
    public NotFoundHttpException(
        string baseMessage,
        string errorMessage,
        Exception baseInnerException,
        AlertVariant variant
    ) : base(baseMessage, errorMessage, baseInnerException, variant, 404) { }

    public NotFoundHttpException(
        string baseMessage,
        AlertVariant variant
    ) : base(baseMessage, variant, 404) { }
}