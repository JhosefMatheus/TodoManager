using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectTests : BaseTests
{
    private readonly ProjectService projectService;

    public ProjectTests() : base()
    {
        projectService = serviceProvider.GetService<ProjectService>()
            ?? throw new NullReferenceException("Não foi possível pegar o serviço de projetos.");
    }

    [TestMethod]
    public void CreateProjectTest()
    {
        try
        {
            CreateProjectResponse createProjectResponse = ProjectUtils.CreateProject(projectService);

            Assert.IsTrue(
                createProjectResponse.Message == ProjectConstants.CreateProjectSuccessMessage,
                "Esperasse que o projeto tenha sido criado com sucesso."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void CheckProjectExistsTest()
    {
        try
        {
            string projectTestName = ProjectUtils.GetProjectTestName();

            CheckProjectExistsResponse checkProjectExistsResponse = projectService.CheckProjectExists(projectTestName);

            Assert.IsFalse(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto não exista.");

            ProjectUtils.CreateProject(projectService);

            checkProjectExistsResponse = projectService.CheckProjectExists(projectTestName);

            Assert.IsTrue(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto exista.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void CheckProjectNameChangedTest()
    {
        try
        {
            CheckProjectNameChangedQuery checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(null);

            Assert.ThrowsException<NotFoundHttpException>(() =>
            {
                projectService.CheckProjectNameChanged(checkProjectNameChangedQueryTest);
            },
            "Esperasse que o projeto não seja encontrado, pois ele ainda não existe.");

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(createdProject.Name, createdProject.Id);

            CheckProjectNameChangedResponse checkProjectNameChangedResponse = projectService
                .CheckProjectNameChanged(checkProjectNameChangedQueryTest);

            Assert.IsFalse(checkProjectNameChangedResponse.Changed, "Esperasse que o nome do projeto não tenha mudado.");

            checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(ProjectUtils.GetProjectUpdateName(), createdProject.Id);

            checkProjectNameChangedResponse = projectService.CheckProjectNameChanged(checkProjectNameChangedQueryTest);

            Assert.IsTrue(checkProjectNameChangedResponse.Changed, "Esperasse que o nome do projeto tenha mudado.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void GetProjectByIdTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(() =>
            {
                projectService.GetProjectById(1);
            },
            "Esperasse que o projeto não seja encontrado, pois ele ainda não existe.");

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            GetProjectByIdResponse getProjectByIdResponse = projectService.GetProjectById(createdProject.Id);

            Assert.IsTrue(
                getProjectByIdResponse.Project.Id == createdProject.Id,
                "Esperasse que os ids dos projetos sejam iguais."
                );

            Assert.IsTrue(
                getProjectByIdResponse.Project.Name == createdProject.Name,
                "Esperasse que os nomes dos projetos sejam iguais."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void UpdateProjectTest()
    {
        try
        {
            UpdateProjectDTO updateProjectTestDTO = ProjectUtils.CreateUpdateProjectTestDTO();

            Assert.ThrowsException<NotFoundHttpException>(
                () =>
                {
                    projectService.UpdateProject(1, updateProjectTestDTO);
                },
                "Esperasse que o projeto não seja encontrado, pois ele ainda não existe."
            );

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            UpdateProjectResponse updateProjectResponse = projectService.UpdateProject(createdProject.Id, updateProjectTestDTO);

            Assert.IsTrue(
                updateProjectResponse.Message == ProjectConstants.UpdateProjectChangedMessage,
                "Esperasse que o projeto tenha sido atualizado."
            );

            updateProjectResponse = projectService.UpdateProject(createdProject.Id, updateProjectTestDTO);

            Assert.IsTrue(
                updateProjectResponse.Message == ProjectConstants.UpdateProjectNotChangedMessage,
                "Esperasse que o projeto não tenha sido atualizado."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void DeleteProjectTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () =>
                {
                    projectService.DeleteProject(1);
                },
                "Esperasse que o projeto não seja encontrado pois ele não existe."
            );

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            DeleteProjectResponse deleteProjectResponse = projectService.DeleteProject(createdProject.Id);

            Assert.IsTrue(
                deleteProjectResponse.Message == ProjectConstants.DeleteProjectSuccessMessage,
                "Esperasse que o projeto tenha sido removido com sucesso."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void ArchiveProjectTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () =>
                {
                    projectService.ArchiveProject(1);
                },
                "Esperasse que o projeto não seja encontrado pois ele ainda não existe."
            );

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            ArchiveProjectResponse archiveProjectResponse = projectService.ArchiveProject(createdProject.Id);

            Assert.IsTrue(
                archiveProjectResponse.Message == ProjectConstants.ArchiveProjectSuccessMessage,
                "Esperasse que o projeto seja arquivado com sucesso."
            );

            archiveProjectResponse = projectService.ArchiveProject(createdProject.Id);

            Assert.IsTrue(
                archiveProjectResponse.Message == ProjectConstants.ProjectAllreadyArchivedMessage,
                "Esperasse que o projeto esteja arquivado."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void UnarchiveProjectTest()
    {
        try
        {
            TodoManagerContext todoManagerContext = ProjectUtils.GetTodoManagerContext(serviceProvider);

            Assert.ThrowsException<NotFoundHttpException>(
                () =>
                {
                    projectService.UnarchiveProject(1);
                },
                "Esperasse que o projeto não seja encontrado pois ele ainda não existe."
            );

            ProjectUtils.CreateProject(projectService);

            ProjectEntity createdProject = ProjectUtils.GetFirstProject(serviceProvider);

            UnarchiveProjectResponse unarchiveProjectResponse = projectService.UnarchiveProject(createdProject.Id);

            Assert.IsTrue(
                unarchiveProjectResponse.Message == ProjectConstants.ProjectAllreadyUnarchivedMessage,
                "Esperasse que o projeto já esteja desarquivado."
            );

            createdProject.Archived = true;

            todoManagerContext.SaveChanges();

            unarchiveProjectResponse = projectService.UnarchiveProject(createdProject.Id);

            Assert.IsTrue(
                unarchiveProjectResponse.Message == ProjectConstants.UnarchiveProjectSuccessMessage,
                "Esperasse que o projeto seja desarquivado com sucesso."
            );
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void GetAllProjectsTest()
    {
        try
        {
            GetAllProjectsResponse getAllProjectsResponse = projectService.GetAllProjects();

            Assert.IsTrue(getAllProjectsResponse.Projects.Count == 0, "Esperasse que nenhum projeto seja encontrado.");

            ProjectUtils.CreateProject(projectService);
            ProjectUtils.CreateProject(projectService);
            ProjectUtils.CreateProject(projectService);

            getAllProjectsResponse = projectService.GetAllProjects();

            Assert.IsTrue(getAllProjectsResponse.Projects.Count == 3, "Esperasse que sejam encontrados três projetos.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }
}