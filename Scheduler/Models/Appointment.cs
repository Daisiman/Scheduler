using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Appointment
    {
        public Doctor Doctor { get; set; }
        public ApplicationUser Patient { get; set; }

        [Key]
        [Column(Order = 1)]
        public int DocotorId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime AppointmentDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public string PatientId { get; set; }

    }
}
