using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Services;
using Scheduler.ViewModels;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private IDoctorList _doctorList;
        private IDoctorWorkHoursList _doctorWorkHoursList;
        private ApplicationDbContext _context;

        public HomeController(IDoctorList doctorList, IDoctorWorkHoursList doctorWorkHoursList, ApplicationDbContext context)
        {
            _doctorWorkHoursList = doctorWorkHoursList;
            _doctorList = doctorList;
            _context = context;
        }

        public JsonResult GetJson()
        {
            var list = new List<SelectOption>
                   {
                       new SelectOption { Value = "1", Text = "10" },
                       new SelectOption { Value = "2", Text = "11" },
                       new SelectOption { Value = "3", Text = "12" },
                       new SelectOption { Value = "4", Text = "13" }
                   };
            return Json(list);
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorList.GetAllDoctorsAsync();
            var doctorsWorkHours = await _doctorWorkHoursList.GetAllDoctorsWorkHoursAsync();

            var model = new DoctorsAndWorkHours()
            {
                DoctorsViewModel = new DoctorsViewModel { Doctors = doctors },
                DoctorsWorkHoursViewModel = new DoctorsWorkHoursViewModel { DoctorWorkHours = doctorsWorkHours }
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor model)
        {
            var doctor = new Doctor()
            {
                Name = model.Name,
                Surname = model.Surname,
                Scope = model.Scope,
                Address = model.Address,
                Office = model.Office
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();



            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public class SelectOption
{
    public String Value { get; set; }
    public String Text { get; set; }
}
