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
        public TaskType? Type { get; set; } // "Habit" або "Goal"
        public TaskAttribute? Attribute { get; set; } // "Strength", "Intelligence", "Charisma"
        public bool IsCompleted { get; set; } = false;
        public double Difficulty { get; set; }
    }

    public enum TaskType { 
        Habbit,
        Goal
    }
    public enum TaskAttribute {
        Strength,
        Intelligence,
        Charisma
    }
}
