using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Models;

namespace Scheduler.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorWorkHours> DoctorWorkHours { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorImage> DoctorImages { get; set; }
        public DbSet<BlackList> BlackList { get; set; }
        public DbSet<LiveChat> LiveChat { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .HasKey(c => new { c.DocotorId, c.AppointmentDate, c.PatientId });

            builder.Entity<BlackList>()
                .HasKey(c => new { c.UserId, c.DateAdded});

            base.OnModelCreating(builder);
        }
    }
}
