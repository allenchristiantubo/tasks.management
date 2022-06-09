using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Batch2022.TaskManagement.Domain.Models.Books
{
    public class Book
    {

        /// <summary>
        /// Book Constructor
        /// </summary>
        public Book()
        {
            BookTitle = string.Empty;
            BookAuthor = string.Empty;
            Publisher = string.Empty;
        }

        /// <summary>
        /// BookID is a Globally Unique Identifier
        /// </summary>
        public Guid BookID { get; set; }

        /// <summary>
        /// Book title is a string and a required property
        /// </summary>
        [Required]
        public string BookTitle { get; set; }

        /// <summary>
        /// Book author is a string and a required property
        /// </summary>
        [Required]
        public string BookAuthor { get; set; }

        /// <summary>
        /// Book publisher is a string and a required property
        /// </summary>
        [Required]
        public string Publisher { get; set; }

        /// <summary>
        /// isDiscontinued is a true or false value
        /// if the the book is discontinued set true else false
        /// </summary>
        public bool IsDiscontinued { get; set; }

        /// <summary>
        /// Publication date of the book
        /// </summary>
        public DateTime DatePublished { get; set; }

    }
}
