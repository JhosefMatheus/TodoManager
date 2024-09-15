using Api.Models.Response;

namespace Api.Models.Responses.Project;

public class GetProjectByIdResponse : BaseResponse
{
    public required ProjectByIdResponse Project { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            project = Project.ToJson(),
        };
    }
}