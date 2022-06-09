using Microsoft.EntityFrameworkCore;

namespace Batch2022.TaskManagement.Infra
{
    public interface ITasksDbContext
    {
        DbSet<Domain.Models.Tasks.Task> Tasks { get; set; }
        DbSet<Domain.Models.Tasks.Tag> Tags { get; set; }
        DbSet<Domain.Models.Books.Book> Books { get; set; }
    }
}
