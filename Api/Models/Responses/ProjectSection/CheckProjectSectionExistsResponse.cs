using Api.Models.Response;

namespace Api.Models.Responses.ProjectSection;

public class CheckProjectSectionExistsResponse : BaseResponse
{
    public required bool ProjectSectionExists { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            projectSectionExists = ProjectSectionExists,
        };
    }
}