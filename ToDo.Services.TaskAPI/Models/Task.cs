using System.ComponentModel.DataAnnotations;

namespace ToDo.Services.TaskAPI.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public bool IsCompleted { get; set; } = false;
        [Required]  
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
