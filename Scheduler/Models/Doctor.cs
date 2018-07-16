using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Doctor
    {
        // img
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Scope { get; set; }
        public WorkHours WorkHours { get; set; }
    }
}
