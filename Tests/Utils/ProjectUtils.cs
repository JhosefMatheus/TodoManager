using Api.Controllers;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Models.Shared;
using Api.Services;

namespace Tests.Utils;

public class ProjectUtils
{
    public static void CreateProject()
    {
        ProjectController projectController = GetProjectController();

        CreateProjectDTO createTestProjectDTO = CreateTestProjectDTO();

        projectController.Create(createTestProjectDTO);
    }

    public static ProjectController GetProjectController()
    {
        ProjectService projectService = GetProjectService();

        ProjectController projectController = new ProjectController(projectService);

        return projectController;
    }

    public static ProjectService GetProjectService()
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

    public static CheckProjectNameChangedQuery CreateCheckProjectNameChangedQueryTest(string? name, int id = 1)
    {
        CheckProjectNameChangedQuery checkProjectNameChangedQueryTest = new CheckProjectNameChangedQuery()
        {
            Id = id,
            Name = name ?? GetProjectTestName(),
        };

        return checkProjectNameChangedQueryTest;
    }

    public static CheckProjectNameChangedResponse CheckProjectNameChangedResponseFromObject(object response)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(response, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(response, "variant");
        bool changedPropertyExists = SharedUtils.CheckPropertyExists(response, "changed");

        if (!messagePropertyExists || !variantPropertyExists || !changedPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para CheckProjectNameChangedResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(response, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(response, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        bool projectNameChanged = SharedUtils.GetPropertyValue<bool>(response, "changed");

        CheckProjectNameChangedResponse checkProjectNameChangedResponse = new CheckProjectNameChangedResponse()
        {
            Message = message,
            Variant = variant,
            Changed = projectNameChanged,
        };

        return checkProjectNameChangedResponse;
    }

    public static string GetProjectUpdateName()
    {
        string projectUpdateName = "Nome Teste Atualizado";

        return projectUpdateName;
    }

    public static GetProjectByIdResponse GetProjectByIdResponseFromObject(object response)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(response, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(response, "variant");
        bool projectPropertyExists = SharedUtils.CheckPropertyExists(response, "project");

        if (!messagePropertyExists || !variantPropertyExists || !projectPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para GetProjectByIdResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(response, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(response, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        object projectByIdResponseObject = SharedUtils.GetPropertyValue<object>(response, "project");

        ProjectByIdResponse project = ProjectByIdResponseFromObject(projectByIdResponseObject);

        GetProjectByIdResponse getProjectByIdResponse = new GetProjectByIdResponse()
        {
            Message = message,
            Variant = variant,
            Project = project,
        };

        return getProjectByIdResponse;
    }

    private static ProjectByIdResponse ProjectByIdResponseFromObject(object src)
    {
        bool idPropertyExists = SharedUtils.CheckPropertyExists(src, "id");
        bool namePropertyExists = SharedUtils.CheckPropertyExists(src, "name");
        bool archivedPropertyExists = SharedUtils.CheckPropertyExists(src, "archived");
        bool createdAtPropertyExists = SharedUtils.CheckPropertyExists(src, "createdAt");
        bool updatedAtPropertyExists = SharedUtils.CheckPropertyExists(src, "updatedAt");

        bool propertiesDoesNotExists =
            !idPropertyExists
            || !namePropertyExists
            || !archivedPropertyExists
            || !createdAtPropertyExists
            || !updatedAtPropertyExists;

        if (propertiesDoesNotExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para ProjectByIdResponse.");
        }

        int id = SharedUtils.GetPropertyValue<int>(src, "id");
        string name = SharedUtils.GetPropertyValue<string>(src, "name");
        bool archived = SharedUtils.GetPropertyValue<bool>(src, "archived");
        DateTime createdAt = SharedUtils.GetPropertyValue<DateTime>(src, "createdAt");
        DateTime? updatedAt = SharedUtils.GetPropertyValue<DateTime?>(src, "updatedAt");

        ProjectByIdResponse projectByIdResponse = new ProjectByIdResponse()
        {
            Id = id,
            Name = name,
            Archived = archived,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
        };

        return projectByIdResponse;
    }

    public static UpdateProjectDTO CreateUpdateProjectTestDTO()
    {
        UpdateProjectDTO updateProjectTestDTO = new UpdateProjectDTO()
        {
            Name = "Teste atualizado.",
        };

        return updateProjectTestDTO;
    }

    public static UpdateProjectResponse UpdateProjectResponseFromObject(object src)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(src, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(src, "variant");

        if (!messagePropertyExists || !variantPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para UpdateProjectResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(src, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(src, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        UpdateProjectResponse updateProjectResponse = new UpdateProjectResponse()
        {
            Message = message,
            Variant = variant,
        };

        return updateProjectResponse;
    }

    public static DeleteProjectResponse DeleteProjectResponseFromObject(object src)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(src, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(src, "variant");

        if (!messagePropertyExists || !variantPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para DeleteProjectResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(src, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(src, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        DeleteProjectResponse deleteProjectResponse = new DeleteProjectResponse()
        {
            Message = message,
            Variant = variant,
        };

        return deleteProjectResponse;
    }

    public static ArchiveProjectResponse ArchiveProjectResponseFromObject(object src)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(src, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(src, "variant");

        if (!messagePropertyExists || !variantPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para ArchiveProjectResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(src, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(src, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        ArchiveProjectResponse archiveProjectResponse = new ArchiveProjectResponse()
        {
            Message = message,
            Variant = variant,
        };

        return archiveProjectResponse;
    }

    public static UnarchiveProjectResponse UnarchiveProjectResponseFromObject(object src)
    {
        bool messagePropertyExists = SharedUtils.CheckPropertyExists(src, "message");
        bool variantPropertyExists = SharedUtils.CheckPropertyExists(src, "variant");

        if (!messagePropertyExists || !variantPropertyExists)
        {
            throw new InvalidCastException("Não foi possível passar o objeto para UnarchiveProjectResponse.");
        }

        string message = SharedUtils.GetPropertyValue<string>(src, "message");

        string variantText = SharedUtils.GetPropertyValue<string>(src, "variant");
        AlertVariant variant = SharedUtils.GetAlertVariantFromString(variantText);

        UnarchiveProjectResponse unarchiveProjectResponse = new UnarchiveProjectResponse()
        {
            Message = message,
            Variant = variant,
        };

        return unarchiveProjectResponse;
    }
}