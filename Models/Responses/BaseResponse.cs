using TodoManager.Models.Shared;

namespace TodoManager.Models.Response;

public abstract class BaseResponse
{
    public required string Message { get; set; }
    public required AlertVariant Variant { get; set; }

    public virtual object ToObject()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
        };
    }
}