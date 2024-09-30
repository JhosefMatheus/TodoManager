using Api.Models.Response;

namespace Api.Models.Responses.ProjectSection;

public class CheckProjectSectionNameChangedResponse : BaseResponse
{
    public required bool NameChanged { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            nameChanged = NameChanged,
        };
    }
}