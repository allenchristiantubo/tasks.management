using Batch2022.TaskManagement.API.Controllers;
using Batch2022.TaskManagement.Domain.Services;
using Moq;

namespace Batch2022.TaskManagement.API.Tests
{
    public class TasksControllerTest
    {
        [Fact]
        public void GetTasks_MustCallRepositoryFindAll()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();

            var sut = new TasksController(mockTaskRepository.Object);

            // Act
            sut.GetTasks();

            // Assert
            mockTaskRepository.Verify(m => m.FindAll(), Times.Once);
        }

        [Fact]
        public void GetTask_WithExistingID_MustCallRepositoryFindById()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.FindById(It.IsAny<Guid>())).Returns(new Domain.Models.Tasks.Task());

            var sut = new TasksController(mockTaskRepository.Object);

            // Act
           sut.GetTask(It.IsAny<Guid>());

            // Assert
            mockTaskRepository.Verify(m => m.FindById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetTask_WithInvalidTaskId_MustCallRepositoryFindById()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.FindById(It.IsAny<Guid>())).Returns<Domain.Models.Tasks.Task>(null);
            var sut = new TasksController(mockTaskRepository.Object);

            // Act
            sut.GetTask(It.IsAny<Guid>());

            // Assert
            mockTaskRepository.Verify(m => m.FindById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void PostTask_WithValidTask_MustCallRepositoryCreate()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.Create(It.IsAny<Domain.Models.Tasks.Task>())).Returns(new Domain.Models.Tasks.Task());
            var sut = new TasksController(mockTaskRepository.Object);

            // Act
            sut.PostTask(It.IsAny<Domain.Models.Tasks.Task>());

            // Assert
            mockTaskRepository.Verify(m => m.Create(It.IsAny<Domain.Models.Tasks.Task>()), Times.Once);
        }

        [Fact]
        public void UpdateTask_WithValidTask_MustCallRepositoryUpdate()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();

            var sut = new TasksController(mockTaskRepository.Object);

            var taskId = Guid.NewGuid();
            var task = new Domain.Models.Tasks.Task
            {
                TaskId = taskId,
                TaskName = "Study about programming languages",
                TaskDescription = "Research on internet about programming languages"
            };

            mockTaskRepository.Setup(m => m.FindById(taskId)).Returns(task);
            mockTaskRepository.Setup(m => m.Update(taskId, task)).Returns(task);

            // Act
            var actualResult = sut.UpdateTask(taskId, task);

            // Assert
            mockTaskRepository.Verify(m => m.FindById(taskId), Times.Once);
            mockTaskRepository.Verify(m => m.Update(taskId, task), Times.Once);
        }

        [Fact]
        public void UpdateTask_WithNonExistingTaskId_MustNotCallRepositoryUpdate()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.FindById(It.IsAny<Guid>())).Returns<Domain.Models.Tasks.Task>(null);
            var sut = new TasksController(mockTaskRepository.Object);

            var taskId = Guid.NewGuid();
            var task = new Domain.Models.Tasks.Task
            {
                TaskId = taskId,
                TaskName = "Learn swimming",
                TaskDescription = "Attend swimming lessons"
            };
            // Act
            sut.UpdateTask(taskId, task);

            // Assert
            mockTaskRepository.Verify(m => m.FindById(taskId), Times.Once);
            mockTaskRepository.Verify(m => m.Update(taskId, task), Times.Never);
        }

        [Fact]
        public void UpdateTask_WithIdNotEqualToTaskId_MustNotCallRepositoryUpdate()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();

            var sut = new TasksController(mockTaskRepository.Object);

            var taskId = Guid.NewGuid();

            var task = new Domain.Models.Tasks.Task
            {
                TaskId = Guid.NewGuid(),
                TaskName = "Learn swimming",
                TaskDescription = "Attend swimming lessons"
            };

            // Act
            var actualResults = sut.UpdateTask(taskId, task);

            // Assert
            mockTaskRepository.Verify(m => m.FindById(taskId), Times.Never);
            mockTaskRepository.Verify(m => m.Update(taskId, task), Times.Never);
        }

        [Fact]
        public void DeleteTask_WithExistingID_MustCallRepositoryDelete ()
        {
            // Arrange
            Mock<ITaskRepository> mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.FindById(It.IsAny<Guid>())).Returns(new Domain.Models.Tasks.Task());
            mockTaskRepository.Setup(m => m.Delete(It.IsAny<Guid>()));
            
            var sut = new TasksController(mockTaskRepository.Object);

            // Act
            sut.DeleteTask(It.IsAny<Guid>());

            // Assert
            mockTaskRepository.Verify(m => m.Delete(It.IsAny<Guid>()), Times.Once);
            mockTaskRepository.Verify(m => m.FindById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void DeleteTask_WithInvalidTaskId_MustNotCallRepositoryDelete()
        {
            // Arrange
            var mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskRepository.Setup(m => m.FindById(It.IsAny<Guid>())).Returns<Domain.Models.Tasks.Task>(null);
            var sut = new TasksController(mockTaskRepository.Object);

            // Act
            sut.DeleteTask(It.IsAny<Guid>());

            // Assert
            mockTaskRepository.Verify(m => m.Delete(It.IsAny<Guid>()), Times.Never);
            mockTaskRepository.Verify(m => m.FindById(It.IsAny<Guid>()), Times.Once);
        }
    }
}