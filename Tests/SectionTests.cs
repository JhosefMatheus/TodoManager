using Api.Database;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class SectionTests
{
    private readonly SectionService sectionService;
    private readonly ServiceProvider serviceProvider;

    public SectionTests(SectionService sectionService)
    {
        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<TodoManagerContext>(options =>
        {
            options
                .UseInMemoryDatabase("TestTodoManagerDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        services.AddScoped<ProjectService>();

        serviceProvider = services.BuildServiceProvider();

        sectionService = serviceProvider.GetService<SectionService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de seções.");
    }

    [TestMethod]
    public void CreateSectionTest()
    {
        try
        {
            
        }
        finally
        {
            SectionUtils.ClearProjectsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }
}