using System.Text.Json;
using Api.Models.Shared;

namespace Api.Models.Response;

public abstract class BaseResponse
{
    public required string Message { get; set; }
    public required AlertVariant Variant { get; set; }

    public virtual object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
        };
    }
}