using Api.Constants;
using Api.Database;
using Api.Models.DTO.ProjectSection;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class SectionTests
{
    private readonly ProjectSectionService projectSectionService;
    private readonly ProjectService projectService;
    private readonly ServiceProvider serviceProvider;

    public SectionTests()
    {
        ServiceCollection services = new ServiceCollection();

        services.AddDbContext<TodoManagerContext>(options =>
        {
            options
                .UseInMemoryDatabase("TestTodoManagerDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        services.AddScoped<ProjectSectionService>();
        services.AddScoped<ProjectService>();

        serviceProvider = services.BuildServiceProvider();

        projectSectionService = serviceProvider.GetService<ProjectSectionService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de seções.");

        projectService = serviceProvider.GetService<ProjectService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de projetos.");
    }

    [TestMethod]
    public void CreateSectionTest()
    {
        try
        {
            CreateProjectSectionDTO createProjectSectionTestDTO = ProjectSectionUtils.CreateProjectSectionTestDTO();

            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.CreateProjectSection(createProjectSectionTestDTO),
                "Esperasse que o projeto para qual a seção está sendo associada não exista."
            );

            CreateProjectSectionResponse createProjectSectionResponse = ProjectSectionUtils
                .CreateProjectSection(projectService, serviceProvider, projectSectionService);

            Assert.IsTrue(createProjectSectionResponse.Message == ProjectSectionConstants.CreateSectionSuccessMessage);
        }
        finally
        {
            ProjectSectionUtils.ClearProjectsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }
}