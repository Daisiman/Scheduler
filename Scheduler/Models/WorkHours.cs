using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class WorkHours
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Monday { get; set; }
        public DateTime Tuesday { get; set; }
        public DateTime Wednesday { get; set; }
        public DateTime Thursday { get; set; }
        public DateTime Friday { get; set; }
        public DateTime Saturday { get; set; }
        public DateTime Sunday { get; set; }
    }
}
