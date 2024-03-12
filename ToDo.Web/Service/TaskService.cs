using ToDo.Web.Models;
using ToDo.Web.Service.IService;
using ToDo.Web.Utility;
using static ToDo.Web.Utility.SD;

namespace ToDo.Web.Service
{
    public class TaskService : ITaskService
    {
        private readonly IBaseService _baseService;

        public TaskService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateTaskAsync(TaskDto taskDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = taskDto,
                Url = SD.TaskAPIBase + "/api/task"
            });
        }

        public async Task<ResponseDto?> GetTasksByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = SD.TaskAPIBase + "/api/task/GetTasks/" + userId
            });
        }

        public async Task<ResponseDto?> DeleteTaskAsync(int taskId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = SD.TaskAPIBase + "/api/task/" + taskId
            });
        }

        public async Task<ResponseDto?> UpdateTaskAsync(TaskDto taskDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Data = taskDto,
                Url = SD.TaskAPIBase + "/api/task/"
            });
        }

        public async Task<ResponseDto?> GetTaskByIdAsync(int taskId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = SD.TaskAPIBase + "/api/task/" + taskId
            });
        }
    }
}
