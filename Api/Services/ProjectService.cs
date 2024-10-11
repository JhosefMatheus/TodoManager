using Microsoft.EntityFrameworkCore.Storage;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Models.Shared;
using Api.Constants;

namespace Api.Services;

public class ProjectService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public ProjectService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateProjectResponse Create(CreateProjectDTO createProjectDTO)
    {
        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            ProjectEntity newProject = new ProjectEntity()
            {
                Name = createProjectDTO.Name,
            };

            this.todoManagerContext.Projects.Add(newProject);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            CreateProjectResponse response = new CreateProjectResponse()
            {
                Message = ProjectConstants.CreateProjectSuccessMessage,
                Variant = AlertVariant.Success,
            };

            return response;
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectConstants.CreateProjectInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public CheckProjectExistsResponse CheckProjectExists(string name)
    {
        bool projectExists = this.todoManagerContext
            .Projects
            .AsEnumerable<ProjectEntity>()
            .Any((ProjectEntity project) =>
            {
                bool validName = project.Name == name;

                return validName;
            });

        string responseMessage = projectExists
            ? ProjectConstants.ProjectExistsMessage
            : ProjectConstants.ProjectDoesntExistsMessage;

        CheckProjectExistsResponse response = new CheckProjectExistsResponse
        {
            Message = responseMessage,
            Variant = AlertVariant.Info,
            ProjectExists = projectExists,
        };

        return response;
    }

    public GetProjectByIdResponse GetProjectById(int id)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        GetProjectByIdResponse response = new GetProjectByIdResponse()
        {
            Message = ProjectConstants.ProjectFoundMessage,
            Variant = AlertVariant.Success,
            Project = new ProjectByIdResponse()
            {
                Id = project.Id,
                Name = project.Name,
                Archived = project.Archived,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
            }
        };

        return response;
    }

    public CheckProjectNameChangedResponse CheckProjectNameChanged(CheckProjectNameChangedQuery query)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, query.Id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        bool nameChanged = project.Name != query.Name;

        string repsonseMessage = nameChanged
            ? ProjectConstants.ProjectNameChangedMessage
            : ProjectConstants.ProjectNameDidNotChangeMessage;

        CheckProjectNameChangedResponse response = new CheckProjectNameChangedResponse
        {
            Message = repsonseMessage,
            Variant = AlertVariant.Success,
            Changed = nameChanged,
        };

        return response;
    }

    public UpdateProjectResponse UpdateProject(int id, UpdateProjectDTO updateProjectDTO)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        bool projectNameChanged = updateProjectDTO.Name != project.Name;

        if (!projectNameChanged)
        {
            return new UpdateProjectResponse
            {
                Message = ProjectConstants.UpdateProjectNotChangedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            project.Name = updateProjectDTO.Name;
            project.UpdatedAt = DateTime.Now;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UpdateProjectResponse
            {
                Message = ProjectConstants.UpdateProjectChangedMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectConstants.UpdateProjectInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public DeleteProjectResponse DeleteProject(int id)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            this.todoManagerContext.Projects.Remove(project);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new DeleteProjectResponse
            {
                Message = ProjectConstants.DeleteProjectSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                ProjectConstants.DeleteProjectInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public ArchiveProjectResponse ArchiveProject(int id)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        if (project.Archived)
        {
            return new ArchiveProjectResponse()
            {
                Message = ProjectConstants.ProjectAllreadyArchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            project.Archived = true;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new ArchiveProjectResponse()
            {
                Message = ProjectConstants.ArchiveProjectSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                ProjectConstants.ArchiveProjectInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public UnarchiveProjectResponse UnarchiveProject(int id)
    {
        ProjectEntity project = FindById<ProjectEntity>(todoManagerContext.Projects, id)
            ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);

        if (!project.Archived)
        {
            return new UnarchiveProjectResponse()
            {
                Message = ProjectConstants.ProjectAllreadyUnarchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            project.Archived = false;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UnarchiveProjectResponse()
            {
                Message = ProjectConstants.UnarchiveProjectSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            throw new InternalServerErrorHttpException(
                ProjectConstants.UnarchiveProjectInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public GetAllProjectsResponse GetAllProjects()
    {
        List<ProjectEntity> projects = this.todoManagerContext.Projects.ToList<ProjectEntity>();

        string getAllProjectsMessage = projects.Count > 0
            ? ProjectConstants.GetAllProjectsSuccessMessage
            : ProjectConstants.GetAllProjectsNoProjectExists;

        AlertVariant getAllProjectsAlertVariant = projects.Count > 0 ? AlertVariant.Success : AlertVariant.Info;

        return new GetAllProjectsResponse()
        {
            Message = getAllProjectsMessage,
            Variant = getAllProjectsAlertVariant,
            Projects = projects.Select<ProjectEntity, ProjectFromAllProjectsResponse>((ProjectEntity project) =>
            {
                return new ProjectFromAllProjectsResponse()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Archived = project.Archived,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt,
                };
            }).ToList<ProjectFromAllProjectsResponse>(),
        };
    }
}