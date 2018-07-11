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
                Surname = "Vaidauskas"

            };

            var doctor2 = new Doctor
            {
                Name = "Paulius",
                Surname = "Paulauskis"

            };

            return Task.FromResult(new[] { doctor1, doctor2 });
        }
    }
}
