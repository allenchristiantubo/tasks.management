using Batch2022.TaskManagement.Domain.Models.Books;
using Batch2022.TaskManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.API.Controllers
{
    /// <summary>
    /// API for managing books
    /// </summary>
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository bookRepository; 
        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        /// <summary>
        /// Get all books that can be found in the list
        /// </summary>
        /// <returns>Returns list of books</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = bookRepository.FindAll();
            return Ok(books);
        }

        /// <summary>
        /// Get book in the list by finding its ID and willreturn the found book
        /// </summary>
        /// <param name="id"></param>Parameter that will pass to use for finding data by ID
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Book?> GetBook(Guid id)
        {
            var book = bookRepository.FindById(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        /// Create a new book and will return its properties
        /// </summary>
        /// <param name="newBook"></param>Parameter that will pass to create a book
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book newBook)
        {
            var createdBook = bookRepository.Create(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookID }, createdBook);
        }

        /// <summary>
        /// If BookID is not same as param id return BadRequest
        /// and if book to update cannot be found return Not Found
        /// else update the book
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Book?> Update(Guid id, [FromBody] Book book)
        { 
            if(book.BookID != id)
            {
                return BadRequest();
            }

            var bookToUpdate = bookRepository.FindById(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            var updatedBook = bookRepository.Update(id, book);
            return Ok(updatedBook);
        }

        /// <summary>
        /// If no book found by ID return NotFound else delete the book
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var bookToDelete = bookRepository.FindById(id);
            if(bookToDelete == null)
            {
                return NotFound();
            }
            bookRepository.Delete(id);
            return NoContent();
        }
    }
}
