using Api.Models.Response;

namespace Api.Models.Responses.Project;

public class CheckProjectExistsResponse : BaseResponse
{
    public required bool ProjectExists { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            projectExists = ProjectExists,
        };
    }
}