using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch2022.TaskManagement.Domain.Models.Tasks
{
    public class Tag
    {
        public Guid TagId { get; set; }
        [Required]
        public string TagName { get; set; } = string.Empty;
        public Guid TaskId { get; set; }
        public virtual Task? Task { get; set; }
    }
}
