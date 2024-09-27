using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class ProjectSectionUtils : BaseUtils
{
    public static void ClearProjectsTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.ProjectSections.RemoveRange(todoManagerContext.ProjectSections);

        todoManagerContext.SaveChanges();
    }

    public static CreateProjectSectionResponse CreateProjectSection(
        ProjectService projectService,
        ServiceProvider serviceProvider,
        ProjectSectionService projectSectionService
        )
    {
        ProjectUtils.CreateProject(projectService);

        Project createdProject = ProjectUtils.GetFirstProject(serviceProvider);

        CreateProjectSectionDTO createProjectSectionTestDTO = CreateProjectSectionTestDTO(createdProject.Id);

        CreateProjectSectionResponse createProjectSectionResponse = projectSectionService
            .CreateProjectSection(createProjectSectionTestDTO);

        return createProjectSectionResponse;
    }

    public static CreateProjectSectionDTO CreateProjectSectionTestDTO(int projectId = 1)
    {
        string projectSectionTestName = GetProjectSectionTestName();

        CreateProjectSectionDTO createProjectSectionTestDTO = new CreateProjectSectionDTO()
        {
            ProjectId = projectId,
            Name = projectSectionTestName,
        };

        return createProjectSectionTestDTO;
    }

    public static string GetProjectSectionTestName()
    {
        string projectSectionTestName = "Teste";

        return projectSectionTestName;
    }
}