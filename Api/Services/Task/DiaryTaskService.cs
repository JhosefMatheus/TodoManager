using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Task;
using Api.Models.Shared;
using Api.Constants;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;

namespace Api.Services.Task;

public class DiaryTaskService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public DiaryTaskService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateTaskResponse Create(JObject createDiaryTaskBaseDTO)
    {
        CreateDiaryTaskDTO createDiaryTaskDTO = createDiaryTaskBaseDTO.ToObject<CreateDiaryTaskDTO>()
            ?? throw new InternalServerErrorHttpException(DiaryTaskConstants.InvalidCreateDiaryTaskBaseDTOMessage, AlertVariant.Error);

        bool hasProjectId = createDiaryTaskDTO.ProjectId != null;

        ProjectEntity? projectEntity = null;

        if (hasProjectId)
        {
            projectEntity = FindById<ProjectEntity>(todoManagerContext.Projects, (int)createDiaryTaskDTO.ProjectId!)
                ?? throw new NotFoundHttpException(ProjectConstants.ProjectNotFoundMessage, AlertVariant.Error);
        }

        bool hasProjectSectionId = createDiaryTaskDTO.ProjectSectionId != null;

        ProjectSectionEntity? projectSectionEntity = null;

        if (hasProjectId && hasProjectSectionId)
        {
            projectSectionEntity = FindById<ProjectSectionEntity>(
                    todoManagerContext.ProjectSections,
                    (int)createDiaryTaskDTO.ProjectSectionId!
                )
                ?? throw new NotFoundHttpException(ProjectSectionConstants.ProjectSectionNotFoundMessage, AlertVariant.Error);
        }

        if (createDiaryTaskDTO.Days.Count == 0)
        {
            throw new BadHttpException(DiaryTaskConstants.NoDaysProvidedMessage, AlertVariant.Warning);
        }

        List<int> uniqueDays = createDiaryTaskDTO.Days.ToHashSet<int>().ToList<int>();

        bool hasInvalidDays = HasInvalidDays(uniqueDays);

        if (hasInvalidDays)
        {
            throw new BadHttpException(DiaryTaskConstants.InvalidDaysProvidedMessage, AlertVariant.Warning);
        }

        using IDbContextTransaction todoManagerContextTransaction = todoManagerContext.Database.BeginTransaction();

        try
        {
            TaskEntity diaryTask = new TaskEntity()
            {
                TaskTypeId = createDiaryTaskDTO.TaskTypeId,
                Name = createDiaryTaskDTO.Name,
                Description = createDiaryTaskDTO.Description,
            };

            if (hasProjectId)
            {
                diaryTask.ProjectId = projectEntity!.Id;
            }

            if (hasProjectId && hasProjectSectionId)
            {
                diaryTask.ProjectSectionId = projectSectionEntity!.Id;
            }

            todoManagerContext.Tasks.Add(diaryTask);
            todoManagerContext.SaveChanges();

            uniqueDays.ForEach((int day) =>
            {
                TaskDayEntity taskDay = new TaskDayEntity()
                {
                    TaskId = diaryTask.Id,
                    Day = day,
                };

                todoManagerContext.TaskDays.Add(taskDay);
            });

            todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new CreateTaskResponse()
            {
                Message = DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException(
                DiaryTaskConstants.CreateDiaryTaskInternalServerErrorMessage,
                exception,
                AlertVariant.Error
            );
        }
    }

    public bool HasInvalidDays(List<int> days) => days.Any<int>((int day) => day < 1 || day > 7);
}