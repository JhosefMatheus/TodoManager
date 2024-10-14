using Api.Models.DTO.Task;
using Api.Models.Responses.Task;
using Api.Services.Task;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService taskService;
        private readonly DiaryTaskService diaryTaskService;

        public TaskController(TaskService taskService, DiaryTaskService diaryTaskService)
        {
            this.taskService = taskService;
            this.diaryTaskService = diaryTaskService;
        }

        [HttpPost("diary-task")]
        public ActionResult Create([FromBody] CreateDiaryTaskDTO createTaskDTO)
        {
            CreateTaskResponse createTaskResponse = diaryTaskService.Create(createTaskDTO);

            object response = createTaskResponse.ToJson();

            return Ok(response);
        }

        [HttpPatch("{id}/move-to")]
        public ActionResult MoveTo(int id, [FromBody] MoveTaskToDTO moveTaskToDTO)
        {
            MoveTaskToResponse moveTaskToResponse = taskService.MoveTo(id, moveTaskToDTO);

            object response = moveTaskToResponse.ToJson();

            return Ok(response);
        }

        [HttpPatch("{id}/archive")]
        public ActionResult Archive(int id)
        {
            ArchiveTaskResponse archiveTaskResponse = taskService.Archive(id);

            object response = archiveTaskResponse.ToJson();

            return Ok(response);
        }

        [HttpPatch("{id}/unarchive")]
        public ActionResult Unarchive(int id)
        {
            UnarchiveTaskResponse unarchiveTaskResponse = taskService.Unarchive(id);

            object response = unarchiveTaskResponse.ToJson();

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateTaskDTO updateTaskDTO)
        {
            UpdateTaskResponse updateTaskResponse = taskService.Update(id, updateTaskDTO);

            object response = updateTaskResponse.ToJson();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            DeleteTaskResponse deleteTaskResponse = taskService.Delete(id);

            object response = deleteTaskResponse.ToJson();

            return Ok(response);
        }

        [HttpPost("test")]
        public ActionResult Test([FromBody] JObject baseModel)
        {
            try
            {
                string type = baseModel["type"].ToString().ToLower();

                switch (type)
                {
                    case "day":
                        // Deserializa o modelo para DerivedDayModel
                        var dayModel = baseModel.ToObject<DerivedDayModel>();
                        return Ok(dayModel);

                    case "date":
                        // Deserializa o modelo para DerivedDateModel
                        var dateModel = baseModel.ToObject<DerivedDateModel>();
                        return Ok(dateModel);

                    default:
                        return BadRequest("Unknown type provided");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }
    }

    public class BaseModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class DerivedDayModel : BaseModel
    {
        public List<int> Days { get; set; }
    }

    public class DerivedDateModel : BaseModel
    {
        public DateTime Date { get; set; }
    }
}

