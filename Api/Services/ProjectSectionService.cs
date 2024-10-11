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

public class ProjectSectionService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public ProjectSectionService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateProjectSectionResponse CreateProjectSection(CreateProjectSectionDTO createSectionDTO)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, createSectionDTO.ProjectId)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            ProjectSectionEntity section = new ProjectSectionEntity
            {
                ProjectId = project.Id,
                Name = createSectionDTO.Name,
            };

            this.todoManagerContext.ProjectSections.Add(section);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            CreateProjectSectionResponse createSectionResponse = new CreateProjectSectionResponse()
            {
                Message = ProjectSectionConstants.CreateProjectSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };

            return createSectionResponse;
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.CreateProjectSectionInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public CheckProjectSectionExistsResponse CheckProjectSectionExists(CheckProjectSectionExistsQuery query)
    {
        ProjectSectionEntity? projectSection = this.todoManagerContext
            .ProjectSections
            .AsEnumerable<ProjectSectionEntity>()
            .Where<ProjectSectionEntity>((ProjectSectionEntity currentProjectSection) =>
            {
                bool validProjectId = currentProjectSection.ProjectId == query.ProjectId;
                bool validProjectSectionName = currentProjectSection.Name == query.Name;

                bool validProjectSection = validProjectId && validProjectSectionName;

                return validProjectSection;
            })
            .FirstOrDefault<ProjectSectionEntity>();

        string checkProjectSectionçExistsResponseMessage = projectSection != null
            ? ProjectSectionConstants.ProjectSectionAllreadyExistsMessage
            : ProjectSectionConstants.ProjectSectionDoesnotExistsMessage;

        bool projectSectionExists = projectSection != null;

        CheckProjectSectionExistsResponse checkProjectSectionExistsResponse = new CheckProjectSectionExistsResponse()
        {
            Message = checkProjectSectionçExistsResponseMessage,
            Variant = AlertVariant.Info,
            ProjectSectionExists = projectSectionExists,
        };

        return checkProjectSectionExistsResponse;
    }

    public CheckProjectSectionNameChangedResponse CheckProjectSectionNameChanged(CheckProjectSectionNameChangedQuery query)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, query.Id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        bool nameChanged = projectSection.Name != query.Name;

        string projectSectionNameChangedResponseMessage = nameChanged
            ? ProjectSectionConstants.ProjectSectionNameChangedMessage
            : ProjectSectionConstants.ProjectSectionNameDidNotChange;

        CheckProjectSectionNameChangedResponse checkProjectSectionNameChangedResponse = new CheckProjectSectionNameChangedResponse()
        {
            Message = projectSectionNameChangedResponseMessage,
            Variant = AlertVariant.Info,
            NameChanged = nameChanged,
        };

        return checkProjectSectionNameChangedResponse;
    }

    public UpdateProjectSectionByIdResponse UpdateProjectSectionById(int id, UpdateProjectSectionDTO updateProjectSectionDTO)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        bool nameChanged = projectSection.Name != updateProjectSectionDTO.Name;

        if (!nameChanged)
        {
            return new UpdateProjectSectionByIdResponse()
            {
                Message = ProjectSectionConstants.UpdateProjectSectionNotChangedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            projectSection.Name = updateProjectSectionDTO.Name;
            projectSection.UpdatedAt = DateTime.Now;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UpdateProjectSectionByIdResponse()
            {
                Message = ProjectSectionConstants.UpdateProjectSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.UpdateProjectSectionInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public ArchiveProjectSectionResponse Archive(int id)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        if (projectSection.Archived)
        {
            return new ArchiveProjectSectionResponse()
            {
                Message = ProjectSectionConstants.ProjectSectionAllreadyArchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            projectSection.Archived = true;
            projectSection.UpdatedAt = DateTime.Now;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new ArchiveProjectSectionResponse()
            {
                Message = ProjectSectionConstants.ArchiveProjectSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.ArchiveProjectSectionInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public UnarchiveProjectSectionResponse Unarchive(int id)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        if (!projectSection.Archived)
        {
            return new UnarchiveProjectSectionResponse()
            {
                Message = ProjectSectionConstants.ProjectSectionAllreadyUnarchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            projectSection.Archived = false;
            projectSection.UpdatedAt = DateTime.Now;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UnarchiveProjectSectionResponse()
            {
                Message = ProjectSectionConstants.UnarchiveProjectSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.UnarchiveProjectSectionInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public DeleteProjectSectionResponse Delete(int id)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            todoManagerContext.ProjectSections.Remove(projectSection);
            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new DeleteProjectSectionResponse()
            {
                Message = ProjectSectionConstants.DeleteProjectSectionSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.DeleteProjectSectionInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public MoveProjectSectionToProjectResponse MoveTo(int id, MoveProjectSectionToProjectDTO moveProjectSectionToProjectDTO)
    {
        ProjectSectionEntity projectSection = FindById<ProjectSectionEntity>(todoManagerContext.ProjectSections, id)
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);

        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, moveProjectSectionToProjectDTO.ProjectId)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        if (projectSection.ProjectId == project.Id)
        {
            return new MoveProjectSectionToProjectResponse()
            {
                Message = ProjectSectionConstants.ProjectSectionAllreadyBelongsToProject,
                Variant = AlertVariant.Info,
            };
        }

        try
        {
            projectSection.ProjectId = project.Id;
            projectSection.UpdatedAt = DateTime.Now;

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new MoveProjectSectionToProjectResponse()
            {
                Message = ProjectSectionConstants.MoveProjectSectionToProjectSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectSectionConstants.MoveProjectSectionToProjectSuccessMessage,
                exception,
                AlertVariant.Error
            );
        }
    }
}