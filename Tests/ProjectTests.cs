using Api.Controllers;
using Api.Models.Database;
using Api.Models.DTO.Project;
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
            ProjectController projectController = ProjectUtils.GetProjectController();

            CreateProjectDTO createTestProjectDTO = ProjectUtils.CreateTestProjectDTO();

            projectController.Create(createTestProjectDTO);

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
            Assert.IsInstanceOfType
            (
                checkProjectExistsActionResult,
                typeof(OkObjectResult),
                "Esperasse que o resultado seja do tipo OkObjectResult."
            );

            OkObjectResult okResult = (OkObjectResult)checkProjectExistsActionResult;

            Assert.IsNotNull(okResult, "Esperasse que o OkObjectResult não seja nulo.");
            Assert.IsNotNull(okResult.Value, "Esperasse que o OkObjectResult.Value não seja nulo.");

            object jsonResponse = okResult.Value;

            CheckProjectExistsResponse checkProjectExistsResponse = ProjectUtils.CheckProjectExistsResponseFromObject(jsonResponse);

            Assert.IsFalse(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto não exista.");

            CreateProjectDTO createTestProjectDTO = ProjectUtils.CreateTestProjectDTO();

            projectController.Create(createTestProjectDTO);

            checkProjectExistsActionResult = projectController.CheckProjectExists(projectTestName);

            Assert.IsInstanceOfType
            (
                checkProjectExistsActionResult,
                typeof(OkObjectResult),
                "Esperasse que o resultado seja do tipo OkObjectResult."
            );

            okResult = (OkObjectResult)checkProjectExistsActionResult;

            Assert.IsNotNull(okResult, "Esperasse que o OkObjectResult não seja nulo.");
            Assert.IsNotNull(okResult.Value, "Esperasse que o OkObjectResult.Value não seja nulo.");

            jsonResponse = okResult.Value;

            checkProjectExistsResponse = ProjectUtils.CheckProjectExistsResponseFromObject(jsonResponse);

            Assert.IsTrue(checkProjectExistsResponse.ProjectExists, "Esperasse que o projeto exista.");
        }
        finally
        {
            ProjectUtils.ClearProjectsTable();
        }

    }
}