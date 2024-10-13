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

    public TaskService(TodoManagerContext todoManagerContext, DiaryTaskService diaryTaskService)
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
            );

            if (projectSectionEntity == null)
            {
                throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Warning);
            }

            try
            {
                taskEntity.ProjectSectionId = projectSectionEntity.Id;

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
            ProjectEntity? projectEntity = FindById<ProjectEntity>(todoManagerContext.Projects, (int)moveTaskToDTO.ProjectId!);

            if (projectEntity == null)
            {
                throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Warning);
            }

            try
            {
                taskEntity.ProjectId = projectEntity.Id;

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
}