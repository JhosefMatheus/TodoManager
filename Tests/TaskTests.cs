using Api.Constants;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Task;
using Api.Services;
using Api.Services.Task;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Tests.Utils;

namespace Tests;

[TestClass]
public class TaskTests : BaseTests
{
    private readonly TaskService taskService;
    private readonly ProjectService projectService;
    private readonly ProjectSectionService projectSectionService;
    private readonly DiaryTaskService diaryTaskService;

    public TaskTests()
    {
        taskService = serviceProvider.GetService<TaskService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de tarefas.");

        projectService = serviceProvider.GetService<ProjectService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de projetos.");

        projectSectionService = serviceProvider.GetService<ProjectSectionService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de seção de projetos.");

        diaryTaskService = serviceProvider.GetService<DiaryTaskService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de tarefas diárias.");
    }

    [TestMethod]
    public void CreateTaskTest()
    {
        try
        {
            JObject createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(1, null, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Create(createDiaryTaskTestDTO),
                "Esperasse que o tipo de tarefa diária não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            CreateTaskResponse createTaskResponse = taskService.Create(createDiaryTaskTestDTO);

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            ProjectSectionEntity createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, 1, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Create(createDiaryTaskTestDTO),
                "Esperasse que o projeto não exista."
            );

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, createdProject.Id, null);

            createTaskResponse = taskService.Create(createDiaryTaskTestDTO);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, createdProjectSection.Id);

            createTaskResponse = taskService.Create(createDiaryTaskTestDTO);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, createdProject.Id, createdProjectSection.Id);

            createTaskResponse = taskService.Create(createDiaryTaskTestDTO);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            DiaryTaskUtils.ClearTaskDayTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
        }
    }

    [TestMethod]
    public void MoveTaskToTest()
    {
        try
        {
            MoveTaskToDTO moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(null, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(1, moveTaskToTestDTO),
                "Esperasse que a tarefa não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            JObject createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            diaryTaskService.Create(createDiaryTaskTestDTO);

            TaskEntity createdTask = TaskUtils.GetFirstEntity(serviceProvider);

            MoveTaskToResponse moveTaskToResponse = taskService.MoveTo(createdTask.Id, moveTaskToTestDTO);

            Assert.IsTrue(
                moveTaskToResponse.Message == TaskConstants.MoveTaskToSuccessMessage,
                "Esperasse que a tarefa tenha sido movida com sucesso."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);
            ProjectSectionEntity createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(1, 1);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(createdTask.Id, moveTaskToTestDTO),
                "Esperasse que a tarefa não possa ser movida para um projeto e seção de projeto inexistentes."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(createdProject.Id, 1);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(createdTask.Id, moveTaskToTestDTO),
                "Esperasse que a tarefa não possa ser movida para uma seção de projeto inexistente."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(1, createdProjectSection.Id);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(createdTask.Id, moveTaskToTestDTO),
                "Esperasse que a tarefa não possa ser movida para um projeto inexistente."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(createdProject.Id, createdProjectSection.Id);

            moveTaskToResponse = taskService.MoveTo(createdTask.Id, moveTaskToTestDTO);

            Assert.IsTrue(
                moveTaskToResponse.Message == TaskConstants.MoveTaskToSuccessMessage,
                "Esperasse que a tarefa tenha sido movida com sucesso."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(1, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(createdTask.Id, moveTaskToTestDTO),
                "Esperasse que a tarefa não possa ser movida para um projeto inexistente."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(createdProject.Id, null);

            moveTaskToResponse = taskService.MoveTo(createdTask.Id, moveTaskToTestDTO);

            Assert.IsTrue(
                moveTaskToResponse.Message == TaskConstants.MoveTaskToSuccessMessage,
                "Esperasse que a tarefa tenha sido movida com sucesso."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(null, 1);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(createdTask.Id, moveTaskToTestDTO),
                "Esperasse que a tarefa não possa ser movida para uma seção de projeto inexistente."
            );

            moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(null, createdProjectSection.Id);

            moveTaskToResponse = taskService.MoveTo(createdTask.Id, moveTaskToTestDTO);

            Assert.IsTrue(
                moveTaskToResponse.Message == TaskConstants.MoveTaskToSuccessMessage,
                "Esperasse que a tarefa tenha sido movida com sucesso."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void ArchiveTaskTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Archive(1),
                "Esperasse que a tarefa não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            JObject createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            diaryTaskService.Create(createDiaryTaskTestDTO);

            TaskEntity createdTask = TaskUtils.GetFirstEntity(serviceProvider);

            ArchiveTaskResponse archiveTaskResponse = taskService.Archive(createdTask.Id);

            Assert.IsTrue(
                archiveTaskResponse.Message == TaskConstants.ArchiveTaskSuccessMessage,
                "Esperasse que a tarefa tenha sido arquivada com sucesso."
            );

            archiveTaskResponse = taskService.Archive(createdTask.Id);

            Assert.IsTrue(
                archiveTaskResponse.Message == TaskConstants.TaskAllreadyArchivedMessage,
                "Esperasse que a tarefa já esteja arquivada."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
        }
    }

    [TestMethod]
    public void UnarchiveTaskTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Unarchive(1),
                "Esperasse que a tarefa não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            JObject createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            diaryTaskService.Create(createDiaryTaskTestDTO);

            TaskEntity createdTask = TaskUtils.GetFirstEntity(serviceProvider);

            UnarchiveTaskResponse unarchiveTaskResponse = taskService.Unarchive(createdTask.Id);

            Assert.IsTrue(
                unarchiveTaskResponse.Message == TaskConstants.TaskAllreadyUnarchivedMessage,
                "Esperasse que a tarefa já esteja desarquivada."
            );

            taskService.Archive(createdTask.Id);

            unarchiveTaskResponse = taskService.Unarchive(createdTask.Id);

            Assert.IsTrue(
                unarchiveTaskResponse.Message == TaskConstants.UnarchiveTaskSuccessMessage,
                "Esperasse que a tarefa já desarquivada com sucesso."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
        }
    }

    [TestMethod]
    public void DeleteTaskTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Delete(1),
                "Esperasse que a tarefa não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            JObject createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            diaryTaskService.Create(createDiaryTaskTestDTO);

            TaskEntity createdTask = TaskUtils.GetFirstEntity(serviceProvider);

            DeleteTaskResponse deleteTaskResponse = taskService.Delete(createdTask.Id);

            Assert.IsTrue(
                deleteTaskResponse.Message == TaskConstants.DeleteTaskSucessMessage,
                "Esperasse que a tarefa seja deletada com sucesso."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
        }
    }

    [TestMethod]
    public void UpdateTaskTest()
    {
        try
        {
            string taskDescription = DiaryTaskUtils.GetDiaryTaskDescription();

            UpdateTaskDTO updateTaskDTO = TaskUtils.CreateUpdateTaskTestDTO(null, taskDescription);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.Update(1, updateTaskDTO),
                "Esperasse que a tarefa não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            TaskTypeEntity diaryTaskType = TaskTypeUtils.GetDiaryTaskTypeEntity(serviceProvider);

            JObject createDiaryTaskDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(diaryTaskType.Id, null, null);

            diaryTaskService.Create(createDiaryTaskDTO);

            TaskEntity createdTask = TaskUtils.GetFirstEntity(serviceProvider);

            UpdateTaskResponse updateTaskResponse = taskService.Update(createdTask.Id, updateTaskDTO);

            Assert.IsTrue(
                updateTaskResponse.Message == TaskConstants.TaskDidNotChangeMessage,
                "Esperasse que a tarefa não tenha sido atualizada pois nada mudou."
            );

            updateTaskDTO = TaskUtils.CreateUpdateTaskTestDTO("New task name", null);

            updateTaskResponse = taskService.Update(createdTask.Id, updateTaskDTO);

            Assert.IsTrue(
                updateTaskResponse.Message == TaskConstants.TaskUpdateSuccessMessage,
                "Esperasse que a tarefa tenha sido atualizada com sucesso."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
            TaskUtils.ClearTaskTable(serviceProvider);
        }
    }
}