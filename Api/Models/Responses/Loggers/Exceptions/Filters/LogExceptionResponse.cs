using Api.Models.Response;

namespace Api.Models.Responses.Loggers.Exceptions.Filters;

public class LogExceptionResponse : BaseResponse
{
    public required bool SuccessfullyLogged { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            successfullyLogged = SuccessfullyLogged,
        };
    }
}