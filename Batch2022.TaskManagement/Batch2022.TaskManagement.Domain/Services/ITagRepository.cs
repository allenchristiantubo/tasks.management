using Tasks = Batch2022.TaskManagement.Domain.Models.Tasks;

namespace Batch2022.TaskManagement.Domain.Services
{
    public interface ITagRepository
    {
        IEnumerable<Tasks.Tag> FindAll();

        Tasks.Tag? FindById(Guid id);

        Tasks.Tag Create(Tasks.Task newTag);

        Tasks.Tag? Update(Guid id, Tasks.Tag tag);

        void Delete(Guid id);
    }
}
