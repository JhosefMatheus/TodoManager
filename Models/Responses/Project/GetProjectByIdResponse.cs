using TodoManager.Models.Response;

namespace TodoManager.Models.Responses.Project;

public class GetProjectByIdResponse : BaseResponse
{
    public ProjectByIdResponse Project { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant,
            project = Project.ToJson(),
        };
    }
}