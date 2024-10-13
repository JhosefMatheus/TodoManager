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
public class DiaryTaskTests : BaseTests
{
    private readonly DiaryTaskService diaryTaskService;
    private readonly ProjectService projectService;
    private readonly ProjectSectionService projectSectionService;

    public DiaryTaskTests() : base()
    {
        diaryTaskService = serviceProvider.GetService<DiaryTaskService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de tarefas diárias.");

        projectService = serviceProvider.GetService<ProjectService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de projetos.");

        projectSectionService = serviceProvider.GetService<ProjectSectionService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de seção de projetos.");
    }

    [TestMethod]
    public void CreateDiaryTaskTest()
    {
        try
        {
            CreateDiaryTaskDTO createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(null, null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => diaryTaskService.Create(createDiaryTaskTestDTO),
                "Esperasse que o tipo de tarefa diária não exista."
            );

            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            CreateTaskResponse createTaskResponse = diaryTaskService.Create(createDiaryTaskTestDTO);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            ProjectSectionEntity createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(createdProject.Id, null);

            createTaskResponse = diaryTaskService.Create(createDiaryTaskTestDTO);

            Assert.IsTrue(
                createTaskResponse.Message == DiaryTaskConstants.CreateDiaryTaskSuccessMessage,
                "Esperasse que a tarefa diária tenha sido criada com sucesso."
            );

            createDiaryTaskTestDTO = DiaryTaskUtils.CreateDiaryTaskTestDTO(createdProject.Id, createdProjectSection.Id);

            createTaskResponse = diaryTaskService.Create(createDiaryTaskTestDTO);

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
}