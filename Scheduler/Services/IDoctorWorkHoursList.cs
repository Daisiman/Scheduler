using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public interface IDoctorWorkHoursList
    {
        Task<DoctorWorkHours[]> GetAllDoctorsWorkHoursAsync();
    }
}
