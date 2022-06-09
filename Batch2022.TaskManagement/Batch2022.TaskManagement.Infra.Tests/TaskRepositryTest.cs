using Batch2022.TaskManagement.Domain.Services;
using Batch2022.TaskManagement.Infra.Repositories;
using Tasks = Batch2022.TaskManagement.Domain.Models.Tasks;
using Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions;
using Batch2022.TaskManagement.Domain;
using Moq;
using Batch2022.TaskManagement.Infra;
using Microsoft.EntityFrameworkCore;

namespace Batch2022.TaskManage.Infra.Tests
{
    public class TaskRepositoryTest
    {
        [Fact]
        public void Create_WithValidData_ReturnsNewObject()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);

            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var task = new Tasks.Task
            {
                TaskName = "Learn cooking",
                TaskDescription = "Learn to cook while taching youtube"
            };

            // Act
            var actualResult = sut.Create(task);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult.TaskId);
            Assert.Equal(Tasks.TaskStatus.New, actualResult.Status);
            Assert.Equal(task.TaskName, actualResult.TaskName);
            Assert.Equal(task.TaskDescription, actualResult.TaskDescription);
            Assert.Equal(DateTime.Today, actualResult.DateCreated);

            // Cleanup
            mockTasksDbContext.Tasks.Remove(task);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Create_WithBlankTaskName_ThrowsInvalidTaskNameArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var task = new Tasks.Task
            {
                TaskName = " ",
                TaskDescription = "Learn to cook while taching youtube"
            };
            
            // Act & Assert
            Assert.True(string.IsNullOrWhiteSpace(task.TaskName));
            Assert.Throws<InvalidTaskNameArgumentException>(() => sut.Create(task));
        }

        [Fact]
        public void Create_WithBlankTaskDescription_ThrowsInvalidTaskDescriptionArgumentException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var task = new Tasks.Task
            {
                TaskName = "Learn cooking",
                TaskDescription = " "
            };

            //Act & Assert
            Assert.True(string.IsNullOrWhiteSpace(task.TaskDescription));
            Assert.Throws<InvalidTaskDescriptionArgumentException>(() => sut.Create(task));
        }

        [Fact]
        public void GetById_WithExistingTaskID_ReturnsTheTask()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTaskDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTaskDbContext);

            var task = new Tasks.Task
            {
                TaskName = "Learn C#",
                TaskDescription = "Start learning C#"
            };
            mockTaskDbContext.Tasks.Add(task);
            mockTaskDbContext.SaveChanges();
            // Act
            var actualResult = sut.FindById(task.TaskId);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult?.TaskId);
            Assert.Equal(Tasks.TaskStatus.New, actualResult?.Status);
            Assert.Equal(task.TaskName, actualResult?.TaskName);
            Assert.Equal(task.TaskDescription, actualResult?.TaskDescription);


            // Cleanup
            mockTaskDbContext.Tasks.Remove(task);
            mockTaskDbContext.SaveChanges();
        }

        [Fact]
        public void GetById_WithNonExistingTaskID_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTaskDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTaskDbContext);

            // Act
            var actualResult = sut.FindById(Guid.NewGuid());

            // Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void FindAll_ReturnsAllTasks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTaskDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTaskDbContext);

            mockTaskDbContext.Add(new Tasks.Task
            {
                TaskName = "Learn C#",
                TaskDescription = "Start learning C#",
            });

            mockTaskDbContext.Add(new Tasks.Task
            {
                TaskName = "Learn Java",
                TaskDescription = "Start learning Java",
            });

            mockTaskDbContext.Add(new Tasks.Task
            {
                TaskName = "Learn TypeScript",
                TaskDescription = "Start learning TypeScript",
            });

            mockTaskDbContext.SaveChanges();
            // Act
            var actualResult = sut.FindAll();

            // Assert
            Assert.Equal(3, actualResult.Count());

            // Cleanup
            mockTaskDbContext.RemoveRange(mockTaskDbContext.Tasks.ToList());
            mockTaskDbContext.SaveChanges();
        }

        [Fact]
        public void Delete_RemovesTheTask()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTaskDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTaskDbContext);

            var task = new Tasks.Task
            {
                TaskName = "Learn C#",
                TaskDescription = "Start learning C#"
            };

            mockTaskDbContext.Add(task);
            mockTaskDbContext.SaveChanges();

            // Act
            sut.Delete(task.TaskId);

            // Assert
            var deletedTask = mockTaskDbContext.Tasks.FirstOrDefault(t => t.TaskId == task.TaskId);
            Assert.Null(deletedTask);
        }

        [Fact]
        public void Update_WithValidData_ReturnsUpdatedTask()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var taskToCreate = new Tasks.Task
            {
                TaskName = "Learn cooking",
                TaskDescription = "Learn to cook while taching youtube"
            };

            mockTasksDbContext.Tasks.Add(taskToCreate);
            mockTasksDbContext.SaveChanges();

            var task = new Tasks.Task
            {
                TaskName = "Learn to swim",
                TaskDescription = "Attend swimming lessons",
            };

            // Act
            var actualResult = sut.Update(taskToCreate.TaskId, task);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult?.TaskId);
            Assert.Equal(taskToCreate.TaskId, actualResult?.TaskId);
            Assert.Equal(task.TaskName, actualResult?.TaskName);
            Assert.Equal(task.TaskDescription, actualResult?.TaskDescription);
            Assert.Equal(task.Status, actualResult?.Status);

            //Cleanup
            mockTasksDbContext.Tasks.Remove(taskToCreate);
            mockTasksDbContext.SaveChanges();
            
        }
        
        [Fact]
        public void Update_WithBlankTaskName_ThrowsInvalidTaskNameArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var taskToCreate = new Tasks.Task
            {
                TaskName = "Learn cooking",
                TaskDescription = "Learn to cook while taching youtube"
            };

            mockTasksDbContext.Tasks.Add(taskToCreate);
            mockTasksDbContext.SaveChanges();

            var task = new Tasks.Task
            {
                TaskName = " ",
                TaskDescription = "Attend swimming lessons",
                Status = Tasks.TaskStatus.Completed,
                DateFinished = DateTime.Today
            };

            // Act & Assert
            Assert.True(string.IsNullOrWhiteSpace(task.TaskName));
            Assert.Throws<InvalidTaskNameArgumentException>(() => sut.Update(taskToCreate.TaskId, task));

            // Cleanup
            mockTasksDbContext.Tasks.Remove(taskToCreate);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Update_WithBlankTaskDescription_ThrowsInvalidTaskDescriptionArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;

            using var mockTasksDbContext = new TasksDbContext(options);
            ITaskRepository sut = new TaskRepository(mockTasksDbContext);

            var taskToCreate = new Tasks.Task
            {
                TaskName = "Learn cooking",
                TaskDescription = "Learn to cook while taching youtube"
            };

            mockTasksDbContext.Tasks.Add(taskToCreate);
            mockTasksDbContext.SaveChanges();

            var task = new Tasks.Task
            {
                TaskName = "Learn swimming",
                TaskDescription = " ",
                Status = Tasks.TaskStatus.Completed,
                DateFinished = DateTime.Today
            };

            // Act & Assert
            Assert.True(string.IsNullOrWhiteSpace(task.TaskDescription));
            Assert.Throws<InvalidTaskDescriptionArgumentException>(() => sut.Update(taskToCreate.TaskId, task));

            // Cleanup
            mockTasksDbContext.Tasks.Remove(taskToCreate);
            mockTasksDbContext.SaveChanges();
        }
    }
}