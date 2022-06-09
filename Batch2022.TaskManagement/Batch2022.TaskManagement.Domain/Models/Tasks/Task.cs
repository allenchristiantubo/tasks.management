using System.ComponentModel.DataAnnotations;

namespace Batch2022.TaskManagement.Domain.Models.Tasks
{
    public class Task
    {
        public Task()
        {
            TaskName = string.Empty;
            TaskDescription = string.Empty;
        }

        public Guid TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }

        /// <summary>
        /// Tags of the task
        /// </summary>
        public virtual ICollection<Tag> Tag { get; set; }
        
        /// <summary>
        /// The date the task was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date the task was updated
        /// </summary>
        public DateTime? DateModified { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateFinished { get; set; }

        public TaskStatus Status { get; set; }
    }
}
