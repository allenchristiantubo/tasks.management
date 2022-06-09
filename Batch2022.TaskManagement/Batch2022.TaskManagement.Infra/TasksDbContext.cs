using Microsoft.EntityFrameworkCore;

namespace Batch2022.TaskManagement.Infra
{
    /// <summary>
    /// dbContext for connecting to data base
    /// </summary>
    public class TasksDbContext : DbContext, ITasksDbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Tasks Collection
        /// </summary>
        public DbSet<Domain.Models.Tasks.Task> Tasks { get; set; } = null;

        public DbSet<Domain.Models.Tasks.Tag> Tags { get; set; } = null;
        /// <summary>
        /// Books Collection
        /// </summary>
        public DbSet<Domain.Models.Books.Book> Books { get; set; } = null;
    }
}
