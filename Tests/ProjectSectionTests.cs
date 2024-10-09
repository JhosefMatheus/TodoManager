using Api.Constants;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.DTO.ProjectSection;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectSectionTests : BaseTests
{
    private readonly ProjectSectionService projectSectionService;
    private readonly ProjectService projectService;

    public ProjectSectionTests() : base()
    {
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

            Assert.IsTrue(createProjectSectionResponse.Message == ProjectSectionConstants.CreateProjectSectionSuccessMessage);
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

    [TestMethod]
    public void UpdateProjectSectionTest()
    {
        try
        {
            UpdateProjectSectionDTO updateProjectSectionTestDTO = ProjectSectionUtils.CreateUpdateProjectSectionTestDTO();

            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.UpdateProjectSectionById(1, updateProjectSectionTestDTO),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            UpdateProjectSectionByIdResponse updateProjectSectionByIdResponse = projectSectionService
                .UpdateProjectSectionById(createdProjectSection.Id, updateProjectSectionTestDTO);

            Assert.IsTrue(
                updateProjectSectionByIdResponse.Message == ProjectSectionConstants.UpdateProjectSectionSuccessMessage,
                "Esperasse que o nome da seção do projeto tenha sido alterado."
            );

            updateProjectSectionByIdResponse = projectSectionService
                .UpdateProjectSectionById(createdProjectSection.Id, updateProjectSectionTestDTO);

            Assert.IsTrue(
                updateProjectSectionByIdResponse.Message == ProjectSectionConstants.UpdateProjectSectionNotChangedMessage,
                "Esperasse que o nome da seção do projeto não tenha sido alterado."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void ArchiveProjectSectionTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.Archive(1),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            ArchiveProjectSectionResponse archiveProjectSectionResponse = projectSectionService.Archive(createdProjectSection.Id);

            Assert.IsTrue(
                archiveProjectSectionResponse.Message == ProjectSectionConstants.ArchiveProjectSectionSuccessMessage,
                "Esperasse que a seção do projeto seja arquivada com sucesso."
            );

            archiveProjectSectionResponse = projectSectionService.Archive(createdProjectSection.Id);

            Assert.IsTrue(
                archiveProjectSectionResponse.Message == ProjectSectionConstants.ProjectSectionAllreadyArchivedMessage,
                "Esperasse que a seção do projeto já esteja arquivada."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void UnarchiveProjectSectionTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.Unarchive(1),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            UnarchiveProjectSectionResponse unarchiveProjectSectionResponse = projectSectionService
                .Unarchive(createdProjectSection.Id);

            Assert.IsTrue(
                unarchiveProjectSectionResponse.Message == ProjectSectionConstants.ProjectSectionAllreadyUnarchivedMessage,
                "Esperasse que a seção do projeto já esteja desarquivada."
            );

            projectSectionService.Archive(createdProjectSection.Id);

            unarchiveProjectSectionResponse = projectSectionService.Unarchive(createdProjectSection.Id);

            Assert.IsTrue(
                unarchiveProjectSectionResponse.Message == ProjectSectionConstants.UnarchiveProjectSectionSuccessMessage,
                "Esperasse que a seção do projeto seja desarquivada com sucesso."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void DeleteProjectSectionTest()
    {
        try
        {
            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.Delete(1),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            DeleteProjectSectionResponse deleteProjectSectionResponse = projectSectionService.Delete(createdProjectSection.Id);

            Assert.IsTrue(
                deleteProjectSectionResponse.Message == ProjectSectionConstants.DeleteProjectSectionSuccessMessage,
                "Esperasse que a seção do projeto tenha sido deletada com sucesso."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }

    [TestMethod]
    public void MoveProjectSectionToProjectTest()
    {
        try
        {
            MoveProjectSectionToProjectDTO moveProjectSectionToProjectTestDTO = ProjectSectionUtils
                .CreateMoveProjectSectionToProjectTestDTO(null);

            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.MoveTo(1, moveProjectSectionToProjectTestDTO),
                "Esperasse que a seção do projeto não exista."
            );

            ProjectSectionUtils.CreateProjectSection(projectService, serviceProvider, projectSectionService);

            ProjectSection createdProjectSection = ProjectSectionUtils.GetFirstProjectSection(serviceProvider);

            Project projectSectionProject = ProjectUtils.GetFirstProject(serviceProvider);

            Assert.ThrowsException<NotFoundHttpException>(
                () => projectSectionService.MoveTo(createdProjectSection.Id, moveProjectSectionToProjectTestDTO),
                "Esperasse que o projeto não exista."
            );

            CreateProjectDTO destinyProjectDTO = new CreateProjectDTO()
            {
                Name = "Destiny project",
            };

            projectService.Create(destinyProjectDTO);

            TodoManagerContext todoManagerContext = serviceProvider.GetService<TodoManagerContext>()!;

            Project destinyProject = todoManagerContext
                .Projects
                .AsEnumerable<Project>()
                .Where<Project>((Project currentProject) =>
                {
                    bool validName = currentProject.Name == "Destiny project";

                    return validName;
                })
                .FirstOrDefault<Project>()!;

            moveProjectSectionToProjectTestDTO = ProjectSectionUtils
                .CreateMoveProjectSectionToProjectTestDTO(projectSectionProject.Id);

            MoveProjectSectionToProjectResponse moveProjectSectionToProjectResponse = projectSectionService
                .MoveTo(createdProjectSection.Id, moveProjectSectionToProjectTestDTO);

            Assert.IsTrue(
                moveProjectSectionToProjectResponse.Message == ProjectSectionConstants.ProjectSectionAllreadyBelongsToProject,
                "Esperasse que a seção já pertença ao projeto."
            );

            moveProjectSectionToProjectTestDTO = ProjectSectionUtils
                .CreateMoveProjectSectionToProjectTestDTO(destinyProject.Id);

            moveProjectSectionToProjectResponse = projectSectionService
                .MoveTo(createdProjectSection.Id, moveProjectSectionToProjectTestDTO);

            Assert.IsTrue(
                moveProjectSectionToProjectResponse.Message == ProjectSectionConstants.MoveProjectSectionToProjectSuccessMessage,
                "Esperasse que a seção seja movida com sucesso para o projeto."
            );
        }
        finally
        {
            ProjectSectionUtils.ClearProjectSectionsTable(serviceProvider);
            ProjectUtils.ClearProjectsTable(serviceProvider);
        }
    }
}