using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Books.Exceptions
{
    public class InvalidPublisherArgumentException : ArgumentException
    {
        public InvalidPublisherArgumentException() : base("Publisher must not be empty", "Publisher"){}
    }
}
