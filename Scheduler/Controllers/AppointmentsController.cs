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
        public IActionResult Appointment(int doctorId)
        {                     
            var appointment = new Appointment()
            {
                DocotorId = doctorId,
                PatientId = _userManager.GetUserId(User),
                AppointmentDate = DateTime.Now
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok();
        }
    }
}