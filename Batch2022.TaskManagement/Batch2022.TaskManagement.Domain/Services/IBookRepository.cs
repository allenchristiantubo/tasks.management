using Batch2022.TaskManagement.Domain.Models.Books;
using Models = Batch2022.TaskManagement.Domain.Models.Books;
namespace Batch2022.TaskManagement.Domain.Services
{
    public interface IBookRepository
    {
        /// <summary>
        /// Find all books
        /// </summary>
        IEnumerable<Models.Books.Book> FindAll();

        /// <summary>
        /// Find book by id
        /// </summary>
        Models.Books.Book? FindById(Guid id);

        /// <summary>
        /// Create a book
        /// </summary>
        Models.Books.Book Create(Book newBook);
        
        /// <summary>
        /// Update a book
        /// </summary>
        Models.Books.Book? Update(Guid id, Book book);

        /// <summary>
        /// Delete a book
        /// </summary>
        void Delete(Guid id);
    }
}
