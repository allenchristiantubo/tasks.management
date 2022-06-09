using Tasks = Batch2022.TaskManagement.Domain.Models.Tasks;

namespace Batch2022.TaskManagement.Domain.Services
{
    public interface ITaskRepository
    {
        IEnumerable<Tasks.Task> FindAll();

        Tasks.Task? FindById(Guid id);

        Tasks.Task Create(Tasks.Task newTask);

        Tasks.Task? Update(Guid id, Tasks.Task task);

        void Delete(Guid id);
    }
}
