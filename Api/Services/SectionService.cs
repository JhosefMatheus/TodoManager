using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Section;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Section;
using Api.Models.Shared;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Services;

public class SectionService
{
    private readonly TodoManagerContext todoManagerContext;
    private readonly ProjectService projectService;

    public SectionService(TodoManagerContext todoManagerContext, ProjectService projectService)
    {
        this.todoManagerContext = todoManagerContext;
        this.projectService = projectService;
    }

    public CreateSectionResponse CreateSection(CreateSectionDTO createSectionDTO)
    {
        Project project = this.projectService.FindProjectById(createSectionDTO.ProjectId)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            Section section = new Section
            {
                ProjectId = project.Id,
                Name = createSectionDTO.Name,
            };

            this.todoManagerContext.Sections.Add(section);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            CreateSectionResponse createSectionResponse = new CreateSectionResponse()
            {
                Message = SectionConstants.CreateSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };

            return createSectionResponse;
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                SectionConstants.CreateSectionInternalServerErrorMessage,
                exception.Message,
                exception,
                AlertVariant.Error
            );
        }
    }
}