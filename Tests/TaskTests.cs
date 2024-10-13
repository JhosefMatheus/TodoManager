using Api.Constants;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Task;
using Api.Services;
using Api.Services.Task;
using Microsoft.Extensions.DependencyInjection;
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
    public void MoveTaskToTest()
    {
        try
        {
            MoveTaskToDTO moveTaskToTestDTO = TaskUtils.CreateMoveTaskToTestDTO(null, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => taskService.MoveTo(1, moveTaskToTestDTO),
                "Esperasse que a tarefa não exista."
            );

            CreateDiaryTaskDTO createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(null, null);

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

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
}