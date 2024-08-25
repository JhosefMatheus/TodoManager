using Microsoft.EntityFrameworkCore.Storage;
using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Project;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Models.Shared;

namespace Api.Services;

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
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                "Erro inesperado ao criar projeto.",
                exception.Message,
                exception,
                AlertVariant.Error
            );
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
        Project project = FindProjectById(id)
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

    public CheckProjectNameChangedResponse CheckProjectNameChanged(CheckProjectNameChangedQuery query)
    {
        Project project = FindProjectById(query.Id)
            ?? throw new NotFoundHttpException("Projeto não encontrado.", AlertVariant.Error);

        bool nameChanged = project.Name != query.Name;

        string repsonseMessage = nameChanged ? "O projeto mudou de nome." : "O projeto não mudou de nome";

        CheckProjectNameChangedResponse response = new CheckProjectNameChangedResponse
        {
            Message = repsonseMessage,
            Variant = AlertVariant.Success,
            Changed = nameChanged,
        };

        return response;
    }

    private Project? FindProjectById(int id)
    {
        Project? project = this.todoManagerContext
            .Projects
            .AsEnumerable<Project>()
            .Where<Project>((Project project) =>
            {
                bool validId = project.Id == id;

                return validId;
            })
            .FirstOrDefault<Project>();

        return project;
    }

    public UpdateProjectResponse UpdateProject(int id, UpdateProjectDTO updateProjectDTO)
    {
        Project project = FindProjectById(id)
            ?? throw new NotFoundHttpException("Projeto não encontrado.", AlertVariant.Error);

        bool projectNameChanged = updateProjectDTO.Name != project.Name;

        if (!projectNameChanged)
        {
            return new UpdateProjectResponse
            {
                Message = "A atualização que você fez não muda nada nos dados do projeto, então o projeto não foi atualizado.",
                Variant = AlertVariant.Info,
            };
        }

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            project.Name = updateProjectDTO.Name;
            project.UpdatedAt = DateTime.Now;

            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new UpdateProjectResponse
            {
                Message = "Projeto atualizado com sucesso.",
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                "Erro inesperado no servidor ao atualizar projeto.",
                exception.Message,
                exception,
                AlertVariant.Error
            );
        }
    }

    public DeleteProjectResponse DeleteProject(int id)
    {
        Project project = FindProjectById(id)
            ?? throw new NotFoundHttpException("Projeto não encontrado.", AlertVariant.Error);

        using IDbContextTransaction todoManagerContextTransaction = this.todoManagerContext.Database.BeginTransaction();

        try
        {
            this.todoManagerContext.Projects.Remove(project);
            this.todoManagerContext.SaveChanges();

            todoManagerContextTransaction.Commit();

            return new DeleteProjectResponse
            {
                Message = "Projeto removido com sucesso.",
                Variant = AlertVariant.Success,
            };
        }
        catch (Exception exception)
        {
            todoManagerContextTransaction.Rollback();

            throw new InternalServerErrorHttpException
            (
                "Erro inesperado no servidor ao remover projeto.",
                exception.Message,
                exception,
                AlertVariant.Error
            );
        }
    }
}