using Api.Models.Response;

namespace Api.Models.Responses.Filters.Exceptions;

public class ExceptionFilterResponse : BaseResponse
{
    public required int StatusCode { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
        };
    }
}