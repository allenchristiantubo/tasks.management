using Tasks = Batch2022.TaskManagement.Domain.Models.Tasks;
using Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions;
using Batch2022.TaskManagement.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Batch2022.TaskManagement.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private TasksDbContext dbContext;
        public TaskRepository(TasksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        /// <param name="newTask">Task to be created.</param>
        /// <returns>An object of new created task.</returns>
        /// <exception cref="InvalidTaskNameArgumentException">
        /// When <paramref name="newTask"/>.TaskName is empty.
        /// </exception>
        /// <exception cref="InvalidTaskDescriptionArgumentException">
        /// When <paramref name="newTask"/>.TaskDescription is empty.
        /// </exception>
        public Tasks.Task Create(Tasks.Task newTask)
        {

            if (string.IsNullOrWhiteSpace(newTask.TaskName))
            {
                throw new InvalidTaskNameArgumentException();
            }

            if(string.IsNullOrWhiteSpace(newTask.TaskDescription))
            {
                throw new InvalidTaskDescriptionArgumentException();
            }

            newTask.TaskId = Guid.NewGuid();
            newTask.DateCreated = DateTime.Today;
            newTask.Status = Tasks.TaskStatus.New;
            newTask.DateModified = null;
            newTask.DateFinished = null;
            dbContext.Tasks.Add(newTask);
            dbContext.SaveChanges();

            return newTask;
        }

        /// <summary>
        /// Find all tasks
        /// </summary>
        /// <returns>List of tasks</returns>
        public IEnumerable<Tasks.Task> FindAll()
        {
            return dbContext.Tasks.Include(task => task.Tag).ToList();
        }

        /// <summary>
        /// Find task by id
        /// </summary>
        /// <param name="id">Parameter that will be used for finding a task</param>
        /// <returns>Object of a found task by id</returns>
        public Tasks.Task? FindById(Guid id)
        {
            return dbContext.Tasks.Include(task => task.Tag).FirstOrDefault(task => task.TaskId == id);
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="id">Parameter that will be used for finding a task</param>
        /// <param name="task">An object to be passed to update a task</param>
        /// <returns>Object of the updated task</returns>
        /// <exception cref="InvalidTaskNameArgumentException">
        /// When <paramref name="task"/>.TaskName is empty.
        /// </exception>
        /// <exception cref="InvalidTaskDescriptionArgumentException">
        /// When <paramref name="task"/>.TaskDescription is empty.
        /// </exception>
        /// <exception cref="InvalidDateFinishedArgumentException">
        /// When <paramref name="task"/>.DateFinished is earlier than DateCompleted.
        /// </exception>
        public Tasks.Task? Update(Guid id, Tasks.Task task)
        {
            
            if(string.IsNullOrWhiteSpace(task.TaskName))
            {
                throw new InvalidTaskNameArgumentException();
            }

            if(string.IsNullOrWhiteSpace(task.TaskDescription))
            {
                throw new InvalidTaskDescriptionArgumentException();
            }

            var taskToUpdate = FindById(id);    

            if (taskToUpdate != null)
            {
                taskToUpdate.TaskName = task.TaskName;
                taskToUpdate.TaskDescription = task.TaskDescription;
                taskToUpdate.Status = task.Status;
                taskToUpdate.DateModified = DateTime.Today;
                if(task.Status == Tasks.TaskStatus.Completed)
                {
                    taskToUpdate.DateFinished = DateTime.Today;
                }
                else if (task.Status != Tasks.TaskStatus.Completed)
                {
                    taskToUpdate.DateFinished = null;
                }
                //taskToUpdate.DateFinished = task.DateFinished;

                dbContext.SaveChanges();
                return taskToUpdate;
            }
            return taskToUpdate; 
        }

        /// <summary>
        /// Delete the task
        /// </summary>
        /// <param name="id">Paramater to be used for finding the task to delete it.</param>
        public void Delete(Guid id)
        {
            var taskToDelete = FindById(id);
            if(taskToDelete != null)
            {
                dbContext.Tasks.Remove(taskToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}