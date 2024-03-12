namespace ToDo.Services.TaskAPI.Models.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string Name { get; set; }
        public PriorityLevel Priority { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }
}
