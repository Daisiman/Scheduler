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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Appointment>()
            //    .HasOne(a => a.Doctor)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasKey(c => new { c.DocotorId, c.AppointmentDate });

            base.OnModelCreating(builder);
        }
    }
}
