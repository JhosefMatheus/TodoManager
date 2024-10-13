using Api.Services;
using Api.Services.Task;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

[TestClass]
public class TaskTests : BaseTests
{
    private readonly TaskService taskService;
    private readonly ProjectService projectService;
    private readonly ProjectSectionService projectSectionService;

    public TaskTests()
    {
        taskService = serviceProvider.GetService<TaskService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de tarefas.");

        projectService = serviceProvider.GetService<ProjectService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de projetos.");

        projectSectionService = serviceProvider.GetService<ProjectSectionService>()
            ?? throw new NullReferenceException("não foi possível encontrar o serviço de seção de projetos.");
    }

    [TestMethod]
    public void MoveTaskToTest()
    {
        try
        {
            
        }
        finally
        {

        }
    }
}