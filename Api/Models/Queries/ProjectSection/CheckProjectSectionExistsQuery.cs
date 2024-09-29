using System.ComponentModel.DataAnnotations;

namespace Api.Models.Queries.ProjectSection;

public class CheckProjectSectionExistsQuery : BaseQuery
{
    [Required(ErrorMessage = "O campo \"projectId\" é obrigatório.")]
    public int ProjectId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório.")]
    public string Name { get; set; }
}