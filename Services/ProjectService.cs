using TodoManager.Database;
using TodoManager.Models.DTO.Project;
using TodoManager.Models.Responses.Project;
using TodoManager.Models.Shared;

namespace TodoManager.Services;

public class ProjectService
{
    private readonly TodoManagerContext todoManagerContext;

    public ProjectService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateProjectResponse Create(CreateProjectDTO createProjectDTO)
    {
        CreateProjectResponse response = new CreateProjectResponse
        {
            Message = "Tarefa criada com sucesso.",
            Variant = AlertVariant.Success,
        };

        return response;
    }
}