using Api.Controllers;
using Api.Database;
using Api.Models.Database;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Microsoft.AspNetCore.Mvc;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectTests
{
    [TestMethod]
    public void CreateProjectTest()
    {
        try
        {
            ProjectUtils.CreateProject();

            List<Project> projects = ProjectUtils.GetProjects();

            Assert.IsTrue(projects.Count == 1, "Ao criar o projeto é esperado que se veja um projeto apenas.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable();
        }

    }

    [TestMethod]
    public void CheckProjectExistsTest()
    {
        try
        {
            ProjectController projectController = ProjectUtils.GetProjectController();

            string projectTestName = ProjectUtils.GetProjectTestName();

            ActionResult checkProjectExistsActionResult = projectController.CheckProjectExists(projectTestName);

            object jsonResponse = SharedUtils.ActionResultToObject(checkProjectExistsActionResult);

            CheckProjectExistsResponse checkProjectExistsResponse = ProjectUtils.CheckProjectExistsResponseFromObject(jsonResponse);

            Assert.IsFalse(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto não exista.");

            ProjectUtils.CreateProject();

            checkProjectExistsActionResult = projectController.CheckProjectExists(projectTestName);

            jsonResponse = SharedUtils.ActionResultToObject(checkProjectExistsActionResult);

            checkProjectExistsResponse = ProjectUtils.CheckProjectExistsResponseFromObject(jsonResponse);

            Assert.IsTrue(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto exista.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable();
        }
    }

    [TestMethod]
    public void CheckProjectNameChangedTest()
    {
        try
        {
            TodoManagerContext todoManagerContext = TodoManagerContextUtils.GetTodoManagerContext();

            ProjectController projectController = ProjectUtils.GetProjectController();

            CheckProjectNameChangedQuery checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(null);

            string checkProjectNameChangedQueryTestBase64 = checkProjectNameChangedQueryTest.ToBase64();

            Assert.ThrowsException<NotFoundHttpException>(() =>
            {
                projectController.CheckProjectNameChanged(checkProjectNameChangedQueryTestBase64);
            },
            "Esperasse que o projeto não seja encontrado, pois ele ainda não existe.");

            ProjectUtils.CreateProject();

            Project createdProject = todoManagerContext.Projects.First<Project>();

            checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(createdProject.Name, createdProject.Id);

            checkProjectNameChangedQueryTestBase64 = checkProjectNameChangedQueryTest.ToBase64();

            ActionResult checkProjectNameChangedActionResult = projectController
                .CheckProjectNameChanged(checkProjectNameChangedQueryTestBase64);

            object jsonResponse = SharedUtils.ActionResultToObject(checkProjectNameChangedActionResult);

            CheckProjectNameChangedResponse checkProjectNameChangedResponse = ProjectUtils
                .CheckProjectNameChangedResponseFromObject(jsonResponse);

            Assert.IsFalse(checkProjectNameChangedResponse.Changed, "Esperasse que o nome do projeto não tenha mudado.");

            checkProjectNameChangedQueryTest = ProjectUtils
                .CreateCheckProjectNameChangedQueryTest(ProjectUtils.GetProjectUpdateName(), createdProject.Id);

            checkProjectNameChangedQueryTestBase64 = checkProjectNameChangedQueryTest.ToBase64();

            checkProjectNameChangedActionResult = projectController
                .CheckProjectNameChanged(checkProjectNameChangedQueryTestBase64);

            jsonResponse = SharedUtils.ActionResultToObject(checkProjectNameChangedActionResult);

            checkProjectNameChangedResponse = ProjectUtils
                .CheckProjectNameChangedResponseFromObject(jsonResponse);

            Assert.IsTrue(checkProjectNameChangedResponse.Changed, "Esperasse que o nome do projeto tenha mudado.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable();
        }
    }

    [TestMethod]
    public void GetProjectByIdTest()
    {
        try
        {
            TodoManagerContext todoManagerContext = TodoManagerContextUtils.GetTodoManagerContext();
            ProjectController projectController = ProjectUtils.GetProjectController();

            Assert.ThrowsException<NotFoundHttpException>(() =>
            {
                projectController.GetProjectById(1);
            },
            "Esperasse que o projeto não seja encontrado, pois ele ainda não existe.");

            ProjectUtils.CreateProject();

            Project createdProject = todoManagerContext.Projects.First<Project>();

            int createdProjectId = createdProject.Id;

            ActionResult getProjectByIdActionResult = projectController.GetProjectById(createdProjectId);

            object jsonResponse = SharedUtils.ActionResultToObject(getProjectByIdActionResult);

            GetProjectByIdResponse getProjectByIdResponse = ProjectUtils.GetProjectByIdResponseFromObject(jsonResponse);

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
            ProjectUtils.ClearProjectsTable();
        }
    }
}