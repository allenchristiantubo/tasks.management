using Tasks = Batch2022.TaskManagement.Domain.Models.Tasks;
using Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions;
using Batch2022.TaskManagement.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Batch2022.TaskManagement.Domain.Models.Tasks;

namespace Batch2022.TaskManagement.Infra.Repositories
{
    public class TagRepository : ITagRepository
    {
        private TasksDbContext dbContext;

        public TagRepository(TasksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Tag Create(Tasks.Task newTag)
        {
            return null;
        }

        public void Delete(Guid id)
        {
            
        }

        public IEnumerable<Tag> FindAll()
        {
            return dbContext.Tags.Include(tag => tag.Task).ToList();
        }

        public Tag? FindById(Guid id)
        {
            return null;
        }

        public Tag? Update(Guid id, Tag tag)
        {
            return null;
        }
    }
}
