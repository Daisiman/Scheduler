using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;
using System.Diagnostics;
using System.IO;
using Scheduler.Services;
using Scheduler.ViewModels;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Text;
using Hangfire;

namespace Scheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/Appointments")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Appointment(int doctorId, string day, string time)
        {            
            var registerTime = new StringBuilder();
            var patientId = _userManager.GetUserId(User);

            day = day.Trim();
            day = Regex.Replace(day, "[A-Za-z )(]", "");

            //get {yyyy-M-dd HH:mm:ss} format
            registerTime.Append(DateTime.Now.Year.ToString()).Append("-").Append(day).Append(" ").Append(time).Append(":00");
            var appointmentDate = DateTime.Parse(registerTime.ToString());

            //Check for dublicates
            if (_context.Appointments.Any(x => x.PatientId == patientId && x.DocotorId == doctorId && x.AppointmentDate == appointmentDate))
            {
                return BadRequest("This appointment already exsist");
            }

            var appointment = new Appointment()
            {
                DocotorId = doctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDate,
                Doctor = _context.Doctors.FirstOrDefault(x => x.Id == doctorId)
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok();
        }
    }
}