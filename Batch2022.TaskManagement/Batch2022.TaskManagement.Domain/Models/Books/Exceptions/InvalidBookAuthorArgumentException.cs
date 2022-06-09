using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Books.Exceptions
{
    public class InvalidBookAuthorArgumentException : ArgumentException
    {
        public InvalidBookAuthorArgumentException() : base("Book author must not be empty", "BookAuthor") { }
    }
}
