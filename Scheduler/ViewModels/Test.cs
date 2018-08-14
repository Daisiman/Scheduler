using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class Test
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<DoctorWorkHours> WorkHours { get; set; }
        public IEnumerable<DoctorImage> Image { get; set; }
    }
}
