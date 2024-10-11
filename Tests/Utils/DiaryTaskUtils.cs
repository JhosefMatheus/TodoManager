using Api.Models.DTO.Task;

namespace Tests.Utils;

public class DiaryTaskUtils : BaseUtils
{
    public static CreateDiaryTaskDTO CreateDiaryTaskTestDTO(int? projectId, int? projectSectionId)
    {
        CreateDiaryTaskDTO createDiaryTaskTestDTO = new CreateDiaryTaskDTO()
        {
            ProjectId = projectId,
            ProjectSectionId = projectSectionId,
            Name = "Diary task test",
            Description = "Diary task test description.",
            Days = new List<int>() { 1, 2, 3, 4, 5, 6, 7 },
        };

        return createDiaryTaskTestDTO;
    }
}