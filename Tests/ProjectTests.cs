using Api.Controllers;
using Tests.Utils;

namespace Tests;

[TestClass]
public class ProjectTests
{
    [TestMethod]
    public void CreateProject()
    {
        ProjectController projectController = ProjectUtils.GetProjectController();
    }
}