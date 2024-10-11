using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class ProjectUtils : BaseUtils
{
    public static CreateProjectResponse CreateProject(ProjectService projectService)
    {
        CreateProjectDTO createTestProjectDTO = CreateTestProjectDTO();

        CreateProjectResponse createProjectResponse = projectService.Create(createTestProjectDTO);

        return createProjectResponse;
    }

    public static CreateProjectDTO CreateTestProjectDTO()
    {
        string projectTestName = GetProjectTestName();

        CreateProjectDTO createTestProjectDTO = new CreateProjectDTO()
        {
            Name = projectTestName,
        };

        return createTestProjectDTO;
    }

    public static string GetProjectTestName()
    {
        string projectTestName = "Teste";

        return projectTestName;
    }

    public static void ClearProjectsTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.Projects.RemoveRange(todoManagerContext.Projects);

        todoManagerContext.SaveChanges();
    }

    public static ProjectEntity GetFirstProject(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        ProjectEntity firstProject = todoManagerContext.Projects.First<ProjectEntity>();

        return firstProject;
    }

    public static CheckProjectNameChangedQuery CreateCheckProjectNameChangedQueryTest(string? name, int id = 1)
    {
        CheckProjectNameChangedQuery checkProjectNameChangedQueryTest = new CheckProjectNameChangedQuery()
        {
            Id = id,
            Name = name ?? GetProjectTestName(),
        };

        return checkProjectNameChangedQueryTest;
    }

    public static string GetProjectUpdateName()
    {
        string projectUpdateName = "Nome Teste Atualizado";

        return projectUpdateName;
    }

    public static UpdateProjectDTO CreateUpdateProjectTestDTO()
    {
        UpdateProjectDTO updateProjectTestDTO = new UpdateProjectDTO()
        {
            Name = "Teste atualizado.",
        };

        return updateProjectTestDTO;
    }
}