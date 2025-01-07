using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbit.Resources.Models
{
    public class Task
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskAttribute Attribute { get; set; }
        public TaskType Type { get; set; }
        public double Score { get; set; }
        public DateTime? CompletionDate { get; set; }

    }
}
