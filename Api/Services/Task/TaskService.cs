using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Task;
using Api.Models.Shared;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Services.Task;

public class TaskService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public TaskService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public MoveTaskToResponse MoveTo(int id, MoveTaskToDTO moveTaskToDTO)
    {
        TaskEntity taskEntity = FindById<TaskEntity>(todoManagerContext.Tasks, id)
            ?? throw new NotFoundHttpException(TaskConstants.TaskNotFoundMessage, AlertVariant.Warning);

        bool hasProjectId = moveTaskToDTO.ProjectId != null;
        bool hasProjectSectionId = moveTaskToDTO.ProjectSectionId != null;

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        if (hasProjectId && hasProjectSectionId)
        {
            ProjectEntity? projectEntity = FindById<ProjectEntity>(todoManagerContext.Projects, (int)moveTaskToDTO.ProjectId!);
            ProjectSectionEntity? projectSectionEntity = FindById<ProjectSectionEntity>(
                todoManagerContext.ProjectSections,
                (int)moveTaskToDTO.ProjectSectionId!
            );

            if (projectEntity == null)
            {
                throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Warning);
            }

            if (projectSectionEntity == null)
            {
                throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Warning);
            }

            try
            {
                taskEntity.ProjectId = projectEntity.Id;
                taskEntity.ProjectSectionId = projectSectionEntity.Id;
                taskEntity.UpdatedAt = DateTime.Now;

                todoManagerContext.SaveChanges();

                todoManagerContextTransaction.Commit();

                return new MoveTaskToResponse()
                {
                    Message = TaskConstants.MoveTaskToSuccessMessage,
                    Variant = AlertVariant.Success,
                };
            }
            catch (Exception exception)
            {
                todoManagerContextTransaction.Rollback();

                throw new InternalServerErrorHttpException(
                    TaskConstants.MoveTaskToInternalServerErrorMessage,
                    exception,
                    AlertVariant.Error
                );
            }
        }
        else if (hasProjectSectionId)
        {
            ProjectSectionEntity? projectSectionEntity = FindById<ProjectSectionEntity>(
                todoManagerContext.ProjectSections,
                (int)moveTaskToDTO.ProjectSectionId!
            )
            ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Warning);

            try
            {
                taskEntity.ProjectSectionId = projectSectionEntity.Id;
                taskEntity.UpdatedAt = DateTime.Now;

                todoManagerContext.SaveChanges();

                todoManagerContextTransaction.Commit();

                return new MoveTaskToResponse()
                {
                    Message = TaskConstants.MoveTaskToSuccessMessage,
                    Variant = AlertVariant.Success,
                };
            }
            catch (Exception exception)
            {
                todoManagerContextTransaction.Rollback();

                throw new InternalServerErrorHttpException(
                    TaskConstants.MoveTaskToInternalServerErrorMessage,
                    exception,
                    AlertVariant.Error
                );
            }
        }
        else if (hasProjectId)
        {
            ProjectEntity? projectEntity = FindById<ProjectEntity>(todoManagerContext.Projects, (int)moveTaskToDTO.ProjectId!)
                ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Warning);

            try
            {
                taskEntity.ProjectId = projectEntity.Id;
                taskEntity.UpdatedAt = DateTime.Now;

                todoManagerContext.SaveChanges();

                todoManagerContextTransaction.Commit();

                return new MoveTaskToResponse()
                {
                    Message = TaskConstants.MoveTaskToSuccessMessage,
                    Variant = AlertVariant.Success,
                };
            }
            catch (Exception exception)
            {
                todoManagerContextTransaction.Rollback();

                throw new InternalServerErrorHttpException(
                    TaskConstants.MoveTaskToInternalServerErrorMessage,
                    exception,
                    AlertVariant.Error
                );
            }
        }
        else
        {
            taskEntity.ProjectId = null;
            taskEntity.ProjectSectionId = null;

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new MoveTaskToResponse()
            {
                Message = TaskConstants.MoveTaskToSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
    }

    public ArchiveTaskResponse Archive(int id)
    {
        TaskEntity taskEntity = FindById<TaskEntity>(todoManagerContext.Tasks, id)
            ?? throw new NotFoundHttpException(TaskConstants.TaskNotFoundMessage, AlertVariant.Error);

        if (taskEntity.Archived)
        {
            return new ArchiveTaskResponse()
            {
                Message = TaskConstants.TaskAllreadyArchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            taskEntity.Archived = true;

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new ArchiveTaskResponse()
            {
                Message = TaskConstants.ArchiveTaskSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                TaskConstants.ArchiveTaskInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public UnarchiveTaskResponse Unarchive(int id)
    {
        TaskEntity taskEntity = FindById<TaskEntity>(todoManagerContext.Tasks, id)
            ?? throw new NotFoundHttpException(TaskConstants.TaskNotFoundMessage, AlertVariant.Error);

        if (!taskEntity.Archived)
        {
            return new UnarchiveTaskResponse()
            {
                Message = TaskConstants.TaskAllreadyUnarchivedMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            taskEntity.Archived = true;

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UnarchiveTaskResponse()
            {
                Message = TaskConstants.UnarchiveTaskSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                TaskConstants.UnarchiveTaskInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public DeleteTaskResponse Delete(int id)
    {
        TaskEntity taskEntity = FindById<TaskEntity>(todoManagerContext.Tasks, id)
            ?? throw new NotFoundHttpException(TaskConstants.TaskNotFoundMessage, AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            todoManagerContext.Tasks.Remove(taskEntity);

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new DeleteTaskResponse()
            {
                Message = TaskConstants.DeleteTaskSucessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                TaskConstants.DeleteTaskInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public UpdateTaskResponse Update(int id, UpdateTaskDTO updateTaskDTO)
    {
        TaskEntity taskEntity = FindById<TaskEntity>(todoManagerContext.Tasks, id)
            ?? throw new NotFoundHttpException(TaskConstants.TaskNotFoundMessage, AlertVariant.Error);

        bool taskNameChanged = updateTaskDTO.Name != taskEntity.Name;
        bool taskDescriptionChanged = updateTaskDTO.Description != taskEntity.Description;

        bool taskChanged = taskNameChanged || taskDescriptionChanged;

        if (!taskChanged)
        {
            return new UpdateTaskResponse()
            {
                Message = TaskConstants.TaskDidNotChangeMessage,
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            if (taskNameChanged)
            {
                taskEntity.Name = updateTaskDTO.Name;
            }

            if (taskDescriptionChanged)
            {
                taskEntity.Description = updateTaskDTO.Description;
            }

            taskEntity.UpdatedAt = DateTime.Now;

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UpdateTaskResponse()
            {
                Message = TaskConstants.TaskUpdateSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                TaskConstants.TaskUpdateInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }
}