using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions
{
    public class InvalidTaskDescriptionArgumentException : ArgumentException
    {
        /// <summary>
        /// Returns an exception when TaskDescription is empty.
        /// </summary>
        public InvalidTaskDescriptionArgumentException() : base("Task description must not be empty.", "TaskDescription")
        {

        }
    }
}
