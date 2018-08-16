using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Scheduler.Data;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.HangfireWorkers
{
    public class SendNotificationJob
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SendNotificationJob(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Execute()
        {
            if(_context.Appointments.Any(x => (x.AppointmentDate.Day - DateTime.Now.Day) > 1))
            {
                Debug.Write("\n YRA appointmentu \n");
            };
        }
    }
}
