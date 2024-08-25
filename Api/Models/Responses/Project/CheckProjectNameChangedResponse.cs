using Api.Models.Response;

namespace Api.Models.Responses.Project;

public class CheckProjectNameChangedResponse : BaseResponse
{
    public bool Changed { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            changed = Changed,
        };
    }
}