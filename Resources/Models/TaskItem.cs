using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbit.Resources.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Type { get; set; } // "Habit" або "Goal"
        public string Attribute { get; set; } // "Strength", "Intelligence", "Charisma"
        public bool IsCompleted { get; set; } = false;
    }
}
