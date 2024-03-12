using ToDo.Web.Models;

namespace ToDo.Web.Service.IService
{
    public interface ITaskService
    {
        Task<ResponseDto?> GetTasksByUserIdAsync(string userId);
        Task<ResponseDto?> GetTaskByIdAsync(int taskId);
        Task<ResponseDto?> CreateTaskAsync(TaskDto taskDto);
        Task<ResponseDto?> UpdateTaskAsync(TaskDto taskDto);
        Task<ResponseDto?> DeleteTaskAsync(int taskId);
    }
}
