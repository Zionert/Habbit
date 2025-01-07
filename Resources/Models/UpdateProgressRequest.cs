using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbit.Resources.Models
{
    public class UpdateProgressRequest
    {
        public TaskAttribute Attribute { get; set; }
        public double Increment { get; set; }
    }

}
