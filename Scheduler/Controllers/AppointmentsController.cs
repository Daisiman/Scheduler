﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Dtos;
using Scheduler.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Scheduler.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Appointments/[action]")]
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
        public IActionResult Appointment(AppointmentDto dto)
        {            
            var registerTime = new StringBuilder();
            var patientId = _userManager.GetUserId(User);
            var appointmentDate = new DateTime();

            var day = dto.Day;

            day = day.Trim();
            day = Regex.Replace(day, "[A-Za-z )(]", "");

            //get {yyyy-M-dd HH:mm:ss} format
            registerTime.Append(DateTime.Now.Year.ToString())
                .Append("-").Append(day)
                .Append(" ").Append(dto.Time)
                .Append(":00");

            if(!(DateTime.TryParse(registerTime.ToString(), out appointmentDate))){
                return BadRequest("Please choose another day");
            }

            //Check for dublicates
            if (_context.Appointments.Any(x => x.PatientId == patientId && x.DocotorId == dto.DoctorId && x.AppointmentDate == appointmentDate))
            {
                return BadRequest("This appointment already exsist");
            }

            var appointment = new Appointment()
            {
                DocotorId = dto.DoctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDate,
                Doctor = _context.Doctors.FirstOrDefault(x => x.Id == dto.DoctorId)
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteAppointment(DeleteAppointmentDto dto)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.DocotorId == dto.DoctorId && x.AppointmentDate == dto.Date);

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public JsonResult GetAppointments()
        {
            //deletes old appointments once a day
            var deleteAppointment = _context.Appointments.Where(x => DateTime.Compare(x.AppointmentDate, DateTime.Now) < 0).ToList();

            foreach(var appointement in deleteAppointment)
            {
                _context.Appointments.Remove(appointement);
                _context.SaveChanges();
            }

            var userId = _userManager.GetUserId(User);
            var appointmets = _context.Appointments.Where(x => x.PatientId == userId && x.AppointmentDate.Month - DateTime.Now.Month == 0 && x.AppointmentDate.Day - DateTime.Now.Day <= 2).ToList();
            return Json(appointmets);
        }

    }
}