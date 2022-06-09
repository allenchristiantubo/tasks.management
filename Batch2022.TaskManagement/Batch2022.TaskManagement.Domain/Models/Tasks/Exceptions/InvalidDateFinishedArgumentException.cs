using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Tasks.Exceptions
{
    public class InvalidDateFinishedArgumentException : ArgumentException
    {
        //Returns an exception when DateFinished is earlier than DateCreated.
        public InvalidDateFinishedArgumentException() : base("Date Finished must not be earlier than Date Created.", "DateFinished")
        {

        }
    }
}
