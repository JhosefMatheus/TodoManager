using Api.Controllers;
using Api.Database;
using Api.Services;

namespace Tests.Utils;

public class ProjectUtils
{
    public ProjectUtils() { }

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
}