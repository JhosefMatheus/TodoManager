namespace Api.Constants;

public class TaskConstants
{
    public const string TaskNotFoundMessage = "Tarefa não encontrada.";
    public const string MoveTaskToSuccessMessage = "Tarefa movida com sucesso.";
    public const string MoveTaskToInternalServerErrorMessage = "Erro inesperado no servidor ao mover a mensagem.";
    public const string MoveTaskProjectIdProjectSectionIdNotProvidedMessage = "Forneça o id do projeto e da seção do projeto.";
    public const string TaskAllreadyArchivedMessage = "A tarefa já está arquivada.";
    public const string TaskAllreadyUnarchivedMessage = "A tarefa já está desarquivada.";
    public const string ArchiveTaskSuccessMessage = "A tarefa foi arquivada com sucesso.";
    public const string ArchiveTaskInternalServerErrorMessage = "Erro inesperado no servidor ao arquivar a tarefa.";
    public const string UnarchiveTaskSuccessMessage = "A tarefa foi desarquivada com sucesso.";
    public const string UnarchiveTaskInternalServerErrorMessage = "Erro inesperado no servidor ao desarquivar a tarefa.";
    public const string DeleteTaskInternalServerErrorMessage = "Erro inesperado no servidor ao excluir a tarefa.";
    public const string DeleteTaskSucessMessage = "A tarefa foi excluída com sucesso.";
    public const string TaskDidNotChangeMessage = "A tarefa não foi atualizada.";
    public const string TaskUpdateInternalServerErrorMessage = "Erro inesperado no servidor ao atualizar a tarefa.";
    public const string TaskUpdateSuccessMessage = "A tarefa foi atualizada com sucesso.";
    public const string CreateBaseTaskDTOParseErrorMessage = "Erro inesperado no servidor ao passar dados de criação de tarefa para modelo desejado.";
}