using Api.Models.Responses.TaskType;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class TaskTypeTests : BaseTests
{
    private readonly TaskTypeService taskTypeService;

    public TaskTypeTests() : base()
    {
        taskTypeService = serviceProvider.GetService<TaskTypeService>()
            ?? throw new NullReferenceException("Não foi possível encontrar o serviço de tipos de tarefas.");
    }

    [TestMethod]
    public void GetAllTaskTypesTest()
    {
        try
        {
            TaskTypeUtils.PopulateTaskTypesTable(serviceProvider);

            GetAllTaskTypesResponse getAllTaskTypesResponse = taskTypeService.GetAll();

            Assert.IsTrue(
                getAllTaskTypesResponse.TaskTypes.Count == 1,
                "Esperasse que seja retornado 1 tipo de tarefa."
            );
        }
        finally
        {
            TaskTypeUtils.ClearTaskTypesTable(serviceProvider);
        }
    }
}