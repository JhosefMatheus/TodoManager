using Api.Controllers;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Services;

namespace Tests.Utils;

public class ProjectUtils
{
    public static ProjectController GetProjectController()
    {
        ProjectService projectService = GetProjectService();

        ProjectController projectController = new ProjectController(projectService);

        return projectController;
    }

    private static ProjectService GetProjectService()
    {
        TodoManagerContext todoManagerContext = TodoManagerContextUtils.GetTodoManagerContext();

        ProjectService projectService = new ProjectService(todoManagerContext);

        return projectService;
    }
    public static CreateProjectDTO CreateTestProjectDTO()
    {
        CreateProjectDTO createTestProjectDTO = new CreateProjectDTO()
        {
            Name = "Teste",
        };

        return createTestProjectDTO;
    }

    public static void ClearProjectsTable()
    {
        using TodoManagerContext todoManagerContext = TodoManagerContextUtils.GetTodoManagerContext();

        List<Project> projects = GetProjects();

        todoManagerContext.Projects.RemoveRange(projects);

        todoManagerContext.SaveChanges();
    }

    public static List<Project> GetProjects()
    {
        TodoManagerContext todoManagerContext = TodoManagerContextUtils.GetTodoManagerContext();

        List<Project> projects = todoManagerContext.Projects.ToList<Project>();

        return projects;
    }
}