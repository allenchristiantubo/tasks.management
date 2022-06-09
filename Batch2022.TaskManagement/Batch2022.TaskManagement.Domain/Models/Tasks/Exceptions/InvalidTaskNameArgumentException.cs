using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions
{
    public class InvalidTaskNameArgumentException : ArgumentException
    {
        /// <summary>
        /// Returns an exception when TaskName is empty.
        /// </summary>
        public InvalidTaskNameArgumentException() : base("Task name must not be empty.", "TaskName")
        {

        }
    }
}
