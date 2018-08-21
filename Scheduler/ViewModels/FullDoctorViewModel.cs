using Scheduler.Models;
using System.Collections.Generic;

namespace Scheduler.ViewModels
{
    public class FullDoctorViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<DoctorWorkHours> WorkHours { get; set; }
        public IEnumerable<DoctorImage> Image { get; set; }
    }
}
