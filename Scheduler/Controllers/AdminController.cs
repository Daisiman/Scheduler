using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Doctor model)
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

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorAndWorkHours model)
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

            var workHours = new DoctorWorkHours()
            {
                Id = lastDoctorId,
                MondayFrom = model.MondayFrom,
                MondayTo = model.MondayTo
            };

            _context.DoctorWorkHours.Add(workHours);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}