using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class DoctorList : IDoctorList
    {
        private ApplicationDbContext _context { get; set; }

        public DoctorList(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor[]> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToArrayAsync();

            return doctors;
        }
    }
}
