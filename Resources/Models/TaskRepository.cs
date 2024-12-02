using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbit.Resources.Models
{
    public static class TaskRepository
    {
        public static List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
