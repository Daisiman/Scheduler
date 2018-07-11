using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Models;
using Scheduler.Services;
using Scheduler.ViewModels;

namespace Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private IDoctorList _doctorList;

        public HomeController(IDoctorList doctorList)
        {
            _doctorList = doctorList;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            //ViewData["Message"] = "Your application description page.";

            var doctors = await _doctorList.GetAllDoctorsAsync();

            var model = new DoctorsViewModel()
            {
                Doctors = doctors
            };

            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
