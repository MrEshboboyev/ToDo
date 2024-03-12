using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.TaskAPI.Data;
using ToDo.Services.TaskAPI.Models.Dto;
using Task = ToDo.Services.TaskAPI.Models.Task;

namespace ToDo.Services.TaskAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize]
    public class TaskAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        protected ResponseDto _response;

        public TaskAPIController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                List<Task> tasks = _db.Tasks.ToList();

                _response.Result = _mapper.Map<List<TaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet("GetTasks/{userId}")]
        public ResponseDto GetTasks(string userId)
        {
            try
            {
                List<Task> tasks = _db.Tasks.Where(task => task.UserId.Equals(userId)).ToList();

                _response.Result = _mapper.Map<List<TaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet("{taskId}")]
        public ResponseDto GetTaskById(int taskId)
        {
            try
            {
                Task task = _db.Tasks.FirstOrDefault(task => task.TaskId == taskId);

                if (task == null)
                {
                    _response.Message = "Task not found";
                }
                else
                {
                    _response.Result = _mapper.Map<TaskDto>(task);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
        
        [HttpPost]
        public ResponseDto Post([FromBody] TaskDto taskDto)
        {
            try
            {
                Task task = _mapper.Map<Task>(taskDto);
                
                _db.Tasks.Add(task);
                _db.SaveChanges();

                _response.Result = _mapper.Map<TaskDto>(task);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
        
        [HttpPut]
        public ResponseDto Put([FromBody] TaskDto taskDto)
        {
            try
            {
                Task task = _mapper.Map<Task>(taskDto);
                
                _db.Tasks.Update(task);
                _db.SaveChanges();

                _response.Result = _mapper.Map<TaskDto>(task);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpDelete("{taskId}")]
        public ResponseDto Delete (int taskId)
        {
            try
            {
                Task task = _db.Tasks.Find(taskId);

                if(task != null)
                {
                    _db.Tasks.Remove(task);
                    _db.SaveChanges();
                }
                else
                {
                    _response.Message = "Task not found";
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
