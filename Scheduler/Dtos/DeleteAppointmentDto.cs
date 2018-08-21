using System;

namespace Scheduler.Dtos
{
    public class DeleteAppointmentDto
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
