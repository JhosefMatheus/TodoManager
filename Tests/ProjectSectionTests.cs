using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.ProjectSection;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectSectionTests
{
    private readonly ProjectSectionService projectSectionService;
    private readonly ProjectService projectService;
    private readonly ServiceProvider serviceProvider;

    public ProjectSectionTests()
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
    public void CreateProjectSectionTest()
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
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void CheckProjectSectionExistsTest()
    {
        try
        {
            CheckProjectSectionExistsQuery checkProjectSectionExistsQuery = ProjectSectionUtils
                .CreateProjectSectionExistsQueryTest(null);

            CheckProjectSectionExistsResponse checkProjectSectionExistsResponse = this.projectSectionService
                .CheckProjectSectionExists(checkProjectSectionExistsQuery);

            Assert.IsFalse(
                checkProjectSectionExistsResponse.ProjectSectionExists,
                "Esperasse que a sessão do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            Project createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            checkProjectSectionExistsQuery = ProjectSectionUtils.CreateProjectSectionExistsQueryTest(null, createdProject.Id);

            checkProjectSectionExistsResponse = this.projectSectionService
                .CheckProjectSectionExists(checkProjectSectionExistsQuery);

            Assert.IsTrue(
                checkProjectSectionExistsResponse.ProjectSectionExists,
                "Esperasse que a sessão do projeto exista."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void CheckProjectSectionNameChangedTest()
    {
        try
        {
            CheckProjectSectionNameChangedQuery checkProjectSectionNameChangedQuery = ProjectSectionUtils
                .CreateProjectSectionNameChangedQueryTest(null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.CheckProjectSectionNameChanged(checkProjectSectionNameChangedQuery),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            checkProjectSectionNameChangedQuery = ProjectSectionUtils
                .CreateProjectSectionNameChangedQueryTest(null, createdProjectSection.Id);

            CheckProjectSectionNameChangedResponse checkProjectSectionNameChangedResponse = projectSectionService
                .CheckProjectSectionNameChanged(checkProjectSectionNameChangedQuery);

            Assert.IsFalse(
                checkProjectSectionNameChangedResponse.NameChanged,
                "Esperasse que o nome da seção do projeto não tenha mudado."
            );

            string projectSectionUpdatedName = ProjectSectionUtils.GetProjectSectionUpdatedName();

            checkProjectSectionNameChangedQuery = ProjectSectionUtils
                .CreateProjectSectionNameChangedQueryTest(projectSectionUpdatedName, createdProjectSection.Id);

            checkProjectSectionNameChangedResponse = projectSectionService
                .CheckProjectSectionNameChanged(checkProjectSectionNameChangedQuery);

            Assert.IsTrue(
                checkProjectSectionNameChangedResponse.NameChanged,
                "Esperasse que o nome da seção do projeto tenha mudado."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }
}