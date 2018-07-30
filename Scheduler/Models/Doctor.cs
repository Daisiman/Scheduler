using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Doctor
    {
        // img
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        [StringLength(80)]
        public string Surname { get; set; }

        [Required]
        [StringLength(80)]
        public string Address { get; set; }

        public string Office { get; set; }

        [Required]
        [StringLength(80)]
        public string Scope { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public virtual DoctorWorkHours DoctorWorkHours { get; set; }

    }
}
