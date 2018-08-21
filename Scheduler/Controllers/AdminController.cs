using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.ViewModels;
using System;
using System.IO;
using System.Linq;

namespace Scheduler.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ApplicationDbContext _context { get; private set; }

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DoctorList()
        {
            return View(_context.Doctors.ToList());
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorAndWorkHours model, IFormFile Image)
        {
            var doctor = new Doctor()
            {
                Name = model.Name,
                Surname = model.Surname,
                Scope = model.Scope,
                Address = model.Address,
                Office = model.Office,
                PhoneNumber = model.PhoneNumber,               
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            var lastDoctorId = doctor.Id;

            if (Image != null)
            {
                if (Image.Length > 0)
                //Convert Image to byte and save to database
                {
                    byte[] p1 = null;
                    using (var fs1 = Image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }

                    var doctorImage = new DoctorImage()
                    {
                        Id = lastDoctorId,
                        Image = p1
                    };

                    _context.DoctorImages.Add(doctorImage);
                    _context.SaveChanges();
                }
            } else
            {
                byte[] photo = System.IO.File.ReadAllBytes("wwwroot/images/blank.png");

                var doctorImage = new DoctorImage()
                {
                    Id = lastDoctorId,
                    Image = photo
                };

                _context.DoctorImages.Add(doctorImage);
                _context.SaveChanges();
            }

            var workHours = new DoctorWorkHours()
            {
                Id = lastDoctorId,
                MondayFrom = model.MondayFrom,
                MondayTo = model.MondayTo,
                TuesdayFrom = model.TuesdayFrom,
                TuesdayTo = model.TuesdayTo,
                WednesdayFrom = model.WednesdayFrom,
                WednesdayTo = model.WednesdayTo,
                ThursdayFrom = model.ThursdayFrom,
                ThursdayTo = model.ThursdayTo,
                FridayFrom = model.FridayFrom,
                FridayTo= model.FridayTo,
                SaturdayFrom = model.SaturdayFrom,
                SaturdayTo = model.SaturdayTo,
                SundayFrom = model.SundayFrom,
                SundayTo = model.SundayTo
            };

            _context.DoctorWorkHours.Add(workHours);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Edit/id
        public ActionResult Edit(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            var workHours = _context.DoctorWorkHours.FirstOrDefault(x => x.Id == id);

            var model = new DoctorAndWorkHours()
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                Scope = doctor.Scope,
                Address = doctor.Address,
                Office = doctor.Office,
                PhoneNumber = doctor.PhoneNumber,
                MondayFrom = workHours.MondayFrom,
                MondayTo = workHours.MondayTo,
                TuesdayFrom = workHours.TuesdayFrom,
                TuesdayTo = workHours.TuesdayTo,
                WednesdayFrom = workHours.WednesdayFrom,
                WednesdayTo = workHours.WednesdayTo,
                ThursdayFrom = workHours.ThursdayFrom,
                ThursdayTo = workHours.ThursdayTo,
                FridayFrom = workHours.FridayFrom,
                FridayTo = workHours.FridayTo,
                SaturdayFrom = workHours.SaturdayFrom,
                SaturdayTo = workHours.SaturdayTo,
                SundayFrom = workHours.SundayFrom,
                SundayTo = workHours.SundayTo
            };

            return View(model);
        }

        // POST: Admin/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, DoctorAndWorkHours model, IFormFile Image)
        {

            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            var workHours = _context.DoctorWorkHours.FirstOrDefault(x => x.Id == id);
            var image = _context.DoctorImages.FirstOrDefault(x => x.Id == id);

            byte[] p1 = null;


            if (Image != null)
            {
                if (Image.Length > 0)
                //Convert Image to byte and save to database
                {
                    //byte[] p1 = null;
                    using (var fs1 = Image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
            }

            try
            {
                //Image
                image.Image = p1;

                //Personal information
                doctor.Name = model.Name;
                doctor.Surname = model.Surname;
                doctor.Address = model.Address;
                doctor.Office = model.Office;
                doctor.Scope = model.Scope;
                doctor.PhoneNumber = model.PhoneNumber;

                //Work hours
                workHours.MondayFrom = model.MondayFrom;
                workHours.MondayTo = model.MondayTo;
                workHours.TuesdayFrom = model.TuesdayFrom;
                workHours.TuesdayTo = model.TuesdayTo;
                workHours.WednesdayFrom = model.WednesdayFrom;
                workHours.WednesdayTo = model.WednesdayTo;
                workHours.ThursdayFrom = model.ThursdayFrom;
                workHours.ThursdayTo = model.ThursdayTo;
                workHours.FridayFrom = model.FridayFrom;
                workHours.FridayTo = model.FridayTo;
                workHours.SaturdayFrom = model.SaturdayFrom;
                workHours.SaturdayTo = model.SaturdayTo;
                workHours.SundayFrom = model.SundayFrom;
                workHours.SundayTo = model.SundayTo;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/id
        public ActionResult Delete(int id)
        {
            return View(_context.Doctors.FirstOrDefault(x => x.Id == id));
        }

        // POST: Admin/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Doctor model)
        {
            var appointments = _context.Appointments.Where(x => x.DocotorId == model.Id);
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == model.Id);

            foreach (var appointment in appointments)
            {
                _context.Appointments.Remove(appointment);
            }

            _context.SaveChanges();

            try
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("About", "Home");
            }
        }

        public ActionResult BlackList()
        {
            return View(_context.BlackList.ToList());
        }

        public ActionResult UsersList()
        {
            return View(_context.Users.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersList(string id)
        {
            var blackListUser = new BlackList()
            {
                UserName = _context.Users.FirstOrDefault(x => x.Id == id).UserName,
                UserId = id,
                DateAdded = DateTime.Now               
            };

            _context.BlackList.Add(blackListUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlackList(string id, DateTime date)
        {
            var blackListUser = _context.BlackList.FirstOrDefault(x => x.UserId == id && x.DateAdded.ToString("yyyy:MM:dd HH:mm:ss") == date.ToString("yyyy:MM:dd HH:mm:ss"));

            _context.BlackList.Remove(blackListUser);
            _context.SaveChanges();


            return RedirectToAction("Index", "Admin");
        }
    }
}