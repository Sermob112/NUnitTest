using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForTests
{
    public class Task
    {
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Contact ContactPerson { get; set; }
        public Employee Author { get; set; }
        public Employee Assignee { get; set; }

        public DateTime? DueDate { get; set; }

    }
}
