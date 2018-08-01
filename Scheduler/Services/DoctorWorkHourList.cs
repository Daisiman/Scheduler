using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class DoctorWorkHourList : IDoctorWorkHoursList
    {
        private ApplicationDbContext _context { get; set; }

        public DoctorWorkHourList(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorWorkHours[]> GetAllDoctorsWorkHoursAsync()
        {
            var doctorWorkHours = await _context.DoctorWorkHours.ToArrayAsync();

            return doctorWorkHours;
        }
    }
}
