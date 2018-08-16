using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class MyAppointment
    {
        public IEnumerable<Appointment> Appointment { get; set; }
        public IEnumerable<Doctor> Doctor { get; set; }
    }
}
