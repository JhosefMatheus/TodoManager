using TodoManager.Database;
using TodoManager.Models.Database;
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

    public CheckProjectExistsResponse CheckProjectExists(string name)
    {
        bool projectExists = this.todoManagerContext
            .Projects
            .AsEnumerable<Project>()
            .Any((Project project) =>
            {
                bool validName = project.Name == name;

                return validName;
            });

        string responseMessage = projectExists ? "Projeto existente." : "Projeto não existe.";

        CheckProjectExistsResponse response = new CheckProjectExistsResponse
        {
            Message = responseMessage,
            Variant = AlertVariant.Info,
            ProjectExists = projectExists,
        };

        return response;
    }
}