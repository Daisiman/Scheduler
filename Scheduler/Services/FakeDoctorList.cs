using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class FakeDoctorList : IDoctorList
    {
        public Task<Doctor[]> GetAllDoctorsAsync()
        {
            var doctor1 = new Doctor
            {
                Name = "Vaidas",
                Surname = "Vaidauskas",
                Scope = "Dentist"
                

            };

            var doctor2 = new Doctor
            {
                Name = "Paulius",
                Surname = "Paulauskis",
                Scope = "Family doctor"

            };

            var doctor3 = new Doctor
            {
                Name = "Benas",
                Surname = "Supis",
                Scope = "Dentist"
                

            };

            var doctor4 = new Doctor
            {
                Name = "Grajus",
                Surname = "Kibinskis",
                Scope = "..."

            };

            return Task.FromResult(new[] { doctor1, doctor2, doctor3, doctor4 });
        }
    }
}
