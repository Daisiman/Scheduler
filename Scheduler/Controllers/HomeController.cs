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

        public async Task<IActionResult> Index()
        {

            //var viewModel = from item in _context.Doctors
            //                join item2 in _context.DoctorWorkHours
            //                on item.Id equals item2.Id
            //                where item.Id.Equals(item2.Id)
            //                select new DoctorsAndWorkHours { new DoctorsViewModel { Doctors = item }, DoctorWorkHours = item2 };

            //return View(viewModel);

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
