using Microsoft.EntityFrameworkCore.Storage;
using TodoManager.Database;
using TodoManager.Models.Database;
using TodoManager.Models.DTO.Project;
using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Responses.Project;
using TodoManager.Models.Shared;

namespace TodoManager.Services;

public class ProjectService
{
    private readonly TodoManagerContext todoManagerContext;

    public ProjectService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateProjectResponse Create(CreateProjectDTO createProjectDTO)
    {
        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            Project newProject = new Project
            {
                Name = createProjectDTO.Name,
            };

            this.todoManagerContext.Projects.Add(newProject);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            CreateProjectResponse response = new CreateProjectResponse
            {
                Message = "Tarefa criada com sucesso.",
                Variant = AlertVariant.Success,
            };

            return response;
        }
        catch (Exception)
        {
            todoManagerContextTransaction.Rollback();
            throw;
        }
    }

    public CheckProjectExistsResponse CheckProjectExists(string name)
    {
        bool projectExists = this.todoManagerContext
            .Projects
            .AsEnumerable<Project>()
            .Any((Project project) =>
            {
                bool validName = project.Name == name;

                return validName;
            });

        string responseMessage = projectExists ? "Projeto existente." : "Projeto não existe.";

        CheckProjectExistsResponse response = new CheckProjectExistsResponse
        {
            Message = responseMessage,
            Variant = AlertVariant.Info,
            ProjectExists = projectExists,
        };

        return response;
    }

    public GetProjectByIdResponse GetProjectById(int id)
    {
        Project project = this.todoManagerContext
            .Projects
            .AsEnumerable<Project>()
            .Where<Project>((Project project) =>
            {
                bool validId = project.Id == id;

                return validId;
            })
            .FirstOrDefault()
            ?? throw new NotFoundHttpException("Projeto não encontrado.", AlertVariant.Error);

        GetProjectByIdResponse response = new GetProjectByIdResponse
        {
            Message = "Projeto encontrado com sucesso.",
            Variant = AlertVariant.Success,
            Project =
            {
                Id = project.Id,
                Name = project.Name,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
            }
        };

        return response;
    }
}