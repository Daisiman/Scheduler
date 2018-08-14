using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public IActionResult Chat()
        {
            return View();
        }

        public JsonResult GetJson(int doctorId, int dayId)
        {
            var list = new List<SelectOption>();
            var workHours = _context.DoctorWorkHours.FirstOrDefault(x => x.Id == doctorId);

            switch (dayId)
            {
                case 0:
                    for (int i = 0; i <= workHours.SundayTo.Value.Hour - workHours.SundayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.SundayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 1:
                    for (int i = 0; i <= workHours.MondayTo.Value.Hour - workHours.MondayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.MondayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 2:
                    for (int i = 0; i <= workHours.TuesdayTo.Value.Hour - workHours.TuesdayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.TuesdayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 3:
                    for (int i = 0; i <= workHours.WednesdayTo.Value.Hour - workHours.WednesdayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.WednesdayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 4:
                    for (int i = 0; i <= workHours.ThursdayTo.Value.Hour - workHours.ThursdayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.ThursdayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 5:
                    for (int i = 0; i <= workHours.FridayTo.Value.Hour - workHours.FridayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.FridayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                case 6:
                    for (int i = 0; i <= workHours.SaturdayTo.Value.Hour - workHours.SaturdayFrom.Value.Hour; i++)
                    {
                        list.Add(new SelectOption { Value = "i", Text = workHours.SaturdayFrom.Value.AddHours(i).ToString("t") });
                    }
                    break;
                default:
                    list = new List<SelectOption>
                    {
                       new SelectOption { Value = "", Text = "--select day--" }
                    };
                    break;

            }
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

            var model2 = new Test()
            {
                Doctors = _context.Doctors.ToList(),
                WorkHours = _context.DoctorWorkHours.ToList(),
                Image = _context.DoctorImages.ToList()
            };

            return View(model2);
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            var model = new Test()
            {
                Doctors = _context.Doctors.ToList(),
                WorkHours = _context.DoctorWorkHours.ToList(),
                Image = _context.DoctorImages.ToList()
            };

            return View(model);
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
