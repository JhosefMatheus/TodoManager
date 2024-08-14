using TodoManager.Models.Response;

namespace TodoManager.Models.Responses.Loggers.Exceptions.Filters;

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