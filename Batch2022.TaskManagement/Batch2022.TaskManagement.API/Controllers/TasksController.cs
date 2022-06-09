using Models = Batch2022.TaskManagement.Domain.Models.Tasks;
using Batch2022.TaskManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Batch2022.TaskManagement.API.Controllers
{
    /// <summary>
    /// API for Managing Tasks
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskRepository taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Task>> GetTasks()
        {
            var tasks = taskRepository.FindAll();
            
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Task?> GetTask(Guid id)
        {
            var task = taskRepository.FindById(id);
            if(task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Models.Task> PostTask([FromBody] Models.Task newTask)
        {
            var createdTask = taskRepository.Create(newTask);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.TaskId }, createdTask);
        }

        /// <summary>
        /// Update the task
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Models.Task?> UpdateTask(Guid id, [FromBody] Models.Task task)
        {
            if(task.TaskId != id)
            {
                return BadRequest();
            }

            if (task.Status != Models.TaskStatus.New && task.Status != Models.TaskStatus.Completed && task.Status != Models.TaskStatus.InProgress)
            {
                return BadRequest();
            }

            var taskToUpdate = taskRepository.FindById(id);
            if(taskToUpdate == null)
            {
                return NotFound();
            }

            var updatedTask = taskRepository.Update(id, task);
            return Ok(updatedTask);
        }

        /// <summary>
        /// If no task found by ID return NotFound else delete the task
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var taskToDelete = taskRepository.FindById(id);
            if(taskToDelete == null)
            {
                return NotFound();
            }
            taskRepository.Delete(id);
            return NoContent();
        }
        
    }
}
