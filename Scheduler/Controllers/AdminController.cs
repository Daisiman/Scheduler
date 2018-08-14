﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.ViewModels;

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
                PhoneNumber = model.PhoneNumber
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
                WednesdayTo = model.WednesdayTo              
            };

            _context.DoctorWorkHours.Add(workHours);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            return View(doctor);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, Doctor model)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);

            try
            {
                doctor.Name = model.Name;
                doctor.Surname = model.Surname;
                doctor.Address = model.Address;
                doctor.Office = model.Office;
                doctor.Scope = model.Scope;
                doctor.PhoneNumber = model.PhoneNumber;

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            return View(doctor);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Doctor doctor)
        {
            try
            {
                _context.Remove(doctor);
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
            return View(_context.Users.ToList());
        }
    }
}