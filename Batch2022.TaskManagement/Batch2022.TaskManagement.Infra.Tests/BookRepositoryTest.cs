using Batch2022.TaskManagement.Domain.Services;
using Batch2022.TaskManagement.Infra.Repositories;
using Books = Batch2022.TaskManagement.Domain.Models.Books;
using Batch2022.TaskManagement.Domain.Models.Books.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Batch2022.TaskManagement.Infra.Tests
{
    public class BookRepositoryTest
    {
        [Fact]
        public void Create_WithValidData_ReturnsNewObject()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = "Harry Potter and the Philosopher's Stone",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            // Act
            var actualResult = sut.Create(book);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult.BookID);
            Assert.Equal(book.BookTitle, actualResult.BookTitle);
            Assert.Equal(book.BookAuthor, actualResult.BookAuthor);
            Assert.Equal(book.Publisher, actualResult.Publisher);
            Assert.Equal(book.IsDiscontinued, actualResult.IsDiscontinued);
            Assert.Equal(book.DatePublished, actualResult.DatePublished);

            // Cleanup
            mockTasksDbContext.Books.Remove(book);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Create_WithBlankBookTitle_ThrowsInvalidBookTitleArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = " ",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.BookTitle));
            Assert.Throws<InvalidBookTitleArgumentException>(() => sut.Create(book));
        }

        [Fact]
        public void Create_WithBlankBookAuthor_ThrowsInvalidBookAuthorArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = "Harry Potter",
                BookAuthor = " ",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.BookAuthor));
            Assert.Throws<InvalidBookAuthorArgumentException>(() => sut.Create(book));
        }

        [Fact]
        public void Create_WithBlankPublisher_ThrowsInvalidPublisherArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = "Harry Potter",
                BookAuthor = "J.K. Rowling",
                Publisher = " ",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.Publisher));
            Assert.Throws<InvalidPublisherArgumentException>(() => sut.Create(book));
        }

        [Fact]
        public void FindById_WithExistingBookID_ReturnsTheBook()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = "Story of my life",
                BookAuthor = "Allen Christian Tubo",
                Publisher = "Tubo Publishing",
                DatePublished = DateTime.Parse("1999-05-09"),
                IsDiscontinued = true
            };

            mockTasksDbContext.Books.Add(book);
            mockTasksDbContext.SaveChanges();

            // Act
            var actualResult = sut.FindById(book.BookID);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult?.BookID);
            Assert.Equal(book.BookTitle, actualResult?.BookTitle);
            Assert.Equal(book.BookAuthor, actualResult?.BookAuthor);
            Assert.Equal(book.Publisher, actualResult?.Publisher);
            Assert.Equal(book.DatePublished, actualResult?.DatePublished);
            Assert.Equal(book.IsDiscontinued, actualResult?.IsDiscontinued);

            // Cleanup
            mockTasksDbContext.Books.Remove(book);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void FindById_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            // Act
            var actualResult = sut.FindById(Guid.NewGuid());

            // Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void FindAll_ReturnAllBooks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            mockTasksDbContext.Add(new Books.Book
            {
                BookTitle = "Story of my Life",
                BookAuthor = "Allen Christian Tubo",
                Publisher = "Tubo Publishing",
                IsDiscontinued = false,
                DatePublished = DateTime.Parse("1999-05-09")
            });

            mockTasksDbContext.Add(new Books.Book
            {
                BookTitle = "K1D x",
                BookAuthor = "Aevin Earl Molina",
                Publisher = "Molina Publishing",
                IsDiscontinued = false,
                DatePublished = DateTime.Parse("1999-09-19")
            });

            mockTasksDbContext.Add(new Books.Book
            {
                BookTitle = "Boracay For All",
                BookAuthor = "Joseph Carlo Sacapano",
                Publisher = "Boracay - Shangri La",
                IsDiscontinued = true,
                DatePublished = DateTime.Parse("1998-09-02")
            });

            mockTasksDbContext.SaveChanges();

            // Act
            var actualResult = sut.FindAll();

            // Assert
            Assert.Equal(3, actualResult.Count());

            // Cleanup
            mockTasksDbContext.Books.RemoveRange(mockTasksDbContext.Books.ToList());
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Delete_RemovesTheBook()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var book = new Books.Book
            {
                BookTitle = "Harry Potter",
                BookAuthor = "J.K Rowling",
                Publisher = "Bloomsbury",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            mockTasksDbContext.Books.Add(book);
            mockTasksDbContext.SaveChanges();

            // Act
            sut.Delete(book.BookID);

            // Assert
            //var deletedBook = mockTasksDbContext.Books.FirstOrDefault(b => b.BookID == book.BookID);
            var deletedBook = sut.FindById(book.BookID);
            Assert.Null(deletedBook);
        }

        [Fact]
        public void Update_WithValidData_ReturnsNewObject()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var bookToCreate = new Books.Book
            {
                BookTitle = "Harry Potter and the Philosopher's Stone",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            mockTasksDbContext.Books.Add(bookToCreate);
            mockTasksDbContext.SaveChanges();

            var book = new Books.Book
            {
                BookTitle = "Introduction to Web Development",
                BookAuthor = "Allen Christian Tubo",
                Publisher = "Tubo Publishing",
                DatePublished = DateTime.Parse("1999-05-09"),
                IsDiscontinued = true
            };

            // Act
            var actualResult = sut.Update(bookToCreate.BookID, book);

            // Assert
            Assert.NotNull(actualResult);
            Assert.NotEqual(Guid.Empty, actualResult?.BookID);
            Assert.Equal(bookToCreate.BookID, actualResult?.BookID);
            Assert.Equal(book.BookTitle, actualResult?.BookTitle);
            Assert.Equal(book.BookAuthor, actualResult?.BookAuthor);
            Assert.Equal(book.Publisher, actualResult?.Publisher);
            Assert.Equal(book.IsDiscontinued, actualResult?.IsDiscontinued);
            Assert.Equal(book.DatePublished, actualResult?.DatePublished);

            // Cleanup
            mockTasksDbContext.Books.Remove(bookToCreate);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Update_WithBlankBookTitle_ThrowsInvalidBookTitleArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var bookToCreate = new Books.Book
            {
                BookTitle = "Harry Potter and the Philosopher's Stone",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };
            
            mockTasksDbContext.Books.Add(bookToCreate);
            mockTasksDbContext.SaveChanges();

            var book = new Books.Book
            {
                BookTitle = " ",
                BookAuthor = "Allen Christian Tubo",
                Publisher = "Tubo Publishing",
                DatePublished = DateTime.Parse("1999-05-09"),
                IsDiscontinued = true
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.BookTitle));
            Assert.Throws<InvalidBookTitleArgumentException>(() => sut.Update(bookToCreate.BookID, book));

            // Cleanup
            mockTasksDbContext.Books.Remove(bookToCreate);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Update_WithBlankBookAuthor_ThrowsInvalidBookAuthorArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var bookToCreate = new Books.Book
            {
                BookTitle = "Harry Potter and the Philosopher's Stone",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            mockTasksDbContext.Books.Add(bookToCreate);
            mockTasksDbContext.SaveChanges();

            var book = new Books.Book
            {
                BookTitle = "Introduction to Web Development",
                BookAuthor = " ",
                Publisher = "Tubo Publishing",
                DatePublished = DateTime.Parse("1999-05-09"),
                IsDiscontinued = true
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.BookAuthor));
            Assert.Throws<InvalidBookAuthorArgumentException>(() => sut.Update(bookToCreate.BookID, book));

            // Cleanup
            mockTasksDbContext.Books.Remove(bookToCreate);
            mockTasksDbContext.SaveChanges();
        }

        [Fact]
        public void Update_WithBlankPublisher_ThrowsInvalidPublisherArgumentException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TasksDbContext>()
                .UseInMemoryDatabase(databaseName: "TasksDb")
                .Options;
            using var mockTasksDbContext = new TasksDbContext(options);
            IBookRepository sut = new BookRepository(mockTasksDbContext);

            var bookToCreate = new Books.Book
            {
                BookTitle = "Harry Potter and the Philosopher's Stone",
                BookAuthor = "J.K. Rowling",
                Publisher = "Publisher",
                DatePublished = DateTime.Parse("2001-07-28"),
                IsDiscontinued = false
            };

            mockTasksDbContext.Books.Add(bookToCreate);
            mockTasksDbContext.SaveChanges();

            var book = new Books.Book
            {
                BookTitle = "Introduction to Web Development",
                BookAuthor = "Allen Christian Tubo",
                Publisher = " ",
                DatePublished = DateTime.Parse("1999-05-09"),
                IsDiscontinued = true
            };

            // Act and Assert
            Assert.True(string.IsNullOrWhiteSpace(book.Publisher));
            Assert.Throws<InvalidPublisherArgumentException>(() => sut.Update(bookToCreate.BookID, book));

            // Cleanup
            mockTasksDbContext.Books.Remove(bookToCreate);
            mockTasksDbContext.SaveChanges();
        }
    }
}
