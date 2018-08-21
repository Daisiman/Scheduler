using Scheduler.Models;
using System.Collections.Generic;

namespace Scheduler.ViewModels
{
    public class MyAppointment
    {
        public IEnumerable<Appointment> Appointment { get; set; }
        public IEnumerable<Doctor> Doctor { get; set; }
    }
}
