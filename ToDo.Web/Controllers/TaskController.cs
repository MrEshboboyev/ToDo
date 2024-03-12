using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using ToDo.Web.Models;
using ToDo.Web.Service.IService;

namespace ToDo.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        public async Task<IActionResult> TaskIndex()
        {
            return View(await LoadTasksBasedOnLoggedInUser());
        }

        // private methods
        private async Task<List<TaskDto>> LoadTasksBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault().Value;
            
            // getting all tasks
            ResponseDto? response = await _taskService.GetTasksByUserIdAsync(userId);
            if(response != null && response.IsSuccess)
            {
                List<TaskDto> tasks = JsonConvert.DeserializeObject<List<TaskDto>>(Convert.ToString(response.Result));

                return tasks;
            }

            return new List<TaskDto>();
        }
    }
}
