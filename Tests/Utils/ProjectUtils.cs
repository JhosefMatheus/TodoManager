using Api.Controllers;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Responses.Project;
using Api.Models.Shared;
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

    public static CheckProjectExistsResponse CheckProjectExistsResponseFromObject(object checkProjectExistsResponseObject)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(checkProjectExistsResponseObject, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(checkProjectExistsResponseObject, "variant");
        bool projectExistsPropertyExists = SharedUtils.CheckPropertyExists(checkProjectExistsResponseObject, "projectExists");

        if (!messagePropertyExists || !variantPropertyExists || !projectExistsPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para CheckProjectExistsResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(checkProjectExistsResponseObject, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(checkProjectExistsResponseObject, "variant");

        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        bool projectExists = SharedUtils.GetPropertyValue<bool>(checkProjectExistsResponseObject, "projectExists");

        CheckProjectExistsResponse checkProjectExistsResponse = new CheckProjectExistsResponse()
        {
            Message = message,
            Variant = variant,
            ProjectExists = projectExists,
        };

        return checkProjectExistsResponse;
    }
}