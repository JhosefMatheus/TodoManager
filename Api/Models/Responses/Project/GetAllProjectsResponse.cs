using Api.Models.Response;

namespace Api.Models.Responses.Project;

public class GetAllProjectsResponse : BaseResponse
{
    public required List<ProjectFromAllProjectsResponse> Projects { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            projects = Projects
                .Select<ProjectFromAllProjectsResponse, object>((ProjectFromAllProjectsResponse project) => project.ToJson()),
        };
    }
}