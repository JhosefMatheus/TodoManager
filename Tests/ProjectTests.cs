using Api.Controllers;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectTests
{
    [TestMethod]
    public void CreateProject()
    {
        ProjectController projectController = ProjectUtils.GetProjectController();

        CreateProjectDTO createTestProjectDTO = ProjectUtils.CreateTestProjectDTO();

        projectController.Create(createTestProjectDTO);

        List<Project> projects = ProjectUtils.GetProjects();

        Assert.IsTrue(projects.Count == 1, "Ao criar o projeto é esperado que se veja um projeto apenas.");

        ProjectUtils.ClearProjectsTable();
    }
}