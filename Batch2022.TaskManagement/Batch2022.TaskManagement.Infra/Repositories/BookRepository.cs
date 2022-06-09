using Models = Batch2022.TaskManagement.Domain.Models.Books;
using Batch2022.TaskManagement.Domain.Services;
using Batch2022.TaskManagement.Domain.Models.Books;
using Batch2022.TaskManagement.Domain.Models.Books.Exceptions;

namespace Batch2022.TaskManagement.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        
        private TasksDbContext dbContext;
        public BookRepository(TasksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Create a book and add it to the list
        /// and will return book created
        /// </summary>
        public Book Create(Book newBook)
        {
            if(string.IsNullOrWhiteSpace(newBook.BookTitle))
            {
                throw new InvalidBookTitleArgumentException();
            }

            if(string.IsNullOrWhiteSpace(newBook.BookAuthor))
            {
                throw new InvalidBookAuthorArgumentException();
            }

            if(string.IsNullOrWhiteSpace(newBook.Publisher))
            {
                throw new InvalidPublisherArgumentException();
            }
            
            dbContext.Books.Add(newBook);
            dbContext.SaveChanges();

            return newBook;
        }

        /// <summary>
        /// Find all books and return the list of books
        /// </summary>
        public IEnumerable<Book> FindAll()
        {
            return dbContext.Books.ToList();
        }

        /// <summary>
        /// Find a book by ID and will return it
        /// </summary>
        public Book? FindById(Guid id)
        {
            return dbContext.Books.FirstOrDefault(book => book.BookID == id);
        }

        /// <summary>
        /// If book found by ID, will update its values and return it
        /// else will return null
        /// </summary>
        public Book? Update(Guid id, Book book)
        {
            if (string.IsNullOrWhiteSpace(book.BookTitle))
            {
                throw new InvalidBookTitleArgumentException();
            }

            if (string.IsNullOrWhiteSpace(book.BookAuthor))
            {
                throw new InvalidBookAuthorArgumentException();
            }

            if (string.IsNullOrWhiteSpace(book.Publisher))
            {
                throw new InvalidPublisherArgumentException();
            }

            var bookToUpdate = FindById(id);

            if(bookToUpdate != null)
            {
                bookToUpdate.BookTitle = book.BookTitle;
                bookToUpdate.BookAuthor = book.BookAuthor;
                bookToUpdate.Publisher = book.Publisher;
                bookToUpdate.IsDiscontinued = book.IsDiscontinued;
                bookToUpdate.DatePublished = book.DatePublished;

                dbContext.SaveChanges();
                return bookToUpdate;
            }
            return null;
        }

        /// <summary>
        /// If book found by ID then delete the book
        /// </summary>
        public void Delete(Guid id)
        {
            var bookToDelete = FindById(id);
            if(bookToDelete != null)
            {
                dbContext.Books.Remove(bookToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}
