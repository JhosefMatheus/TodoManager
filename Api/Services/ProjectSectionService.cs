using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.ProjectSection;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Models.Shared;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Services;

public class ProjectSectionService
{
    private readonly TodoManagerContext todoManagerContext;
    private readonly ProjectService projectService;

    public ProjectSectionService(TodoManagerContext todoManagerContext, ProjectService projectService)
    {
        this.todoManagerContext = todoManagerContext;
        this.projectService = projectService;
    }

    public CreateProjectSectionResponse CreateProjectSection(CreateProjectSectionDTO createSectionDTO)
    {
        Project project = this.projectService.FindProjectById(createSectionDTO.ProjectId)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            ProjectSection section = new ProjectSection
            {
                ProjectId = project.Id,
                Name = createSectionDTO.Name,
            };

            this.todoManagerContext.ProjectSections.Add(section);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            CreateProjectSectionResponse createSectionResponse = new CreateProjectSectionResponse()
            {
                Message = ProjectSectionConstants.CreateSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };

            return createSectionResponse;
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.CreateSectionInternalServerErrorMessage,
                exception.Message,
                exception,
                AlertVariant.Error
            );
        }
    }

    public CheckProjectSectionExistsResponse CheckProjectSectionExists(CheckProjectSectionExistsQuery query)
    {
        ProjectSection? projectSection = this.todoManagerContext
            .ProjectSections
            .AsEnumerable<ProjectSection>()
            .Where<ProjectSection>((ProjectSection currentProjectSection) =>
            {
                bool validProjectId = currentProjectSection.ProjectId == query.ProjectId;
                bool validProjectSectionName = currentProjectSection.Name == query.Name;

                bool validProjectSection = validProjectId && validProjectSectionName;

                return validProjectSection;
            })
            .FirstOrDefault<ProjectSection>();

        string checkProjectSectionçExistsResponseMessage = projectSection != null
            ? "Seção já existe."
            : "Seção não existe.";

        bool projectSectionExists = projectSection != null;

        CheckProjectSectionExistsResponse checkProjectSectionExistsResponse = new CheckProjectSectionExistsResponse()
        {
            Message = checkProjectSectionçExistsResponseMessage,
            Variant = AlertVariant.Info,
            ProjectSectionExists = projectSectionExists,
        };

        return checkProjectSectionExistsResponse;
    }
}