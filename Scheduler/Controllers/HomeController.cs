﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Dtos;
using Scheduler.Models;
using Scheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult MyAppointments()
        {
            var model = new MyAppointment()
            {
                Appointment = _context.Appointments.Where(x => x.PatientId == _userManager.GetUserId(User)),
                Doctor = _context.Doctors.ToList()
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View();
        }

        public JsonResult GetJson(WorkHoursJsonDto dto)
        {
            var selectedDay = new StringBuilder();
            var list = new List<SelectOption>();
            var existingAppointments = new List<String>();

            var day = dto.Day;

            day = Regex.Replace(day, "[A-Za-z )(]", "");

            //Get {yyyy-MM-dd} format
            selectedDay.Append(DateTime.Now.Year).Append("-").Append(day);

            var workHours = _context.DoctorWorkHours.FirstOrDefault(x => x.Id == dto.DoctorId);
            var appointments = _context.Appointments
                .Where(x => x.AppointmentDate.ToShortDateString() == selectedDay.ToString() && x.DocotorId == dto.DoctorId)
                .Select(x => x.AppointmentDate);

            foreach (var date in appointments)
            {
                existingAppointments.Add(date.ToShortTimeString());
            }

            switch (dto.DayId)
            {
                case 0:
                    for (int i = 0; i <= workHours.SundayTo.Value.Hour - workHours.SundayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.SundayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.SundayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 1:
                    for (int i = 0; i <= workHours.MondayTo.Value.Hour - workHours.MondayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.MondayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.MondayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 2:
                    for (int i = 0; i <= workHours.TuesdayTo.Value.Hour - workHours.TuesdayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.TuesdayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.TuesdayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 3:
                    for (int i = 0; i <= workHours.WednesdayTo.Value.Hour - workHours.WednesdayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.WednesdayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.WednesdayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 4:
                    for (int i = 0; i <= workHours.ThursdayTo.Value.Hour - workHours.ThursdayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.ThursdayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.ThursdayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 5:
                    for (int i = 0; i <= workHours.FridayTo.Value.Hour - workHours.FridayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.FridayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.FridayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                case 6:
                    for (int i = 0; i <= workHours.SaturdayTo.Value.Hour - workHours.SaturdayFrom.Value.Hour; i++)
                    {
                        if (existingAppointments.Contains(workHours.SaturdayFrom.Value.AddHours(i).ToShortTimeString()))
                        {
                            continue;
                        }

                        list.Add(new SelectOption { Value = "i", Text = workHours.SaturdayFrom.Value.AddHours(i).ToShortTimeString() });
                    }
                    break;
                default:
                    list = new List<SelectOption>
                    {
                       new SelectOption { Value = "", Text = "--select day--" }
                    };
                    break;

            }

            if (list.Count == 0)
            {
                list.Add(new SelectOption { Value = "", Text = "All times taken. Try another day" });
            }

            return Json(list);
        }

        public IActionResult Index()
        {
            var model = new FullDoctorViewModel()
            {
                Doctors = _context.Doctors.ToList(),
                WorkHours = _context.DoctorWorkHours.ToList(),
                Image = _context.DoctorImages.ToList()
            };

            return View(model);
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
            var image = _context.DoctorImages.FirstOrDefault(x => x.Id == id);

            MemoryStream ms = new MemoryStream(image.Image);

            return new FileStreamResult(ms, "image/png");

        }
    }
}

public class SelectOption
{
    public String Value { get; set; }
    public String Text { get; set; }
}
