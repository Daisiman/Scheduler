using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class DoctorWorkHours
    {
        [ForeignKey("Doctor")]
        public int Id { get; set; }

        public DateTime? MondayFrom { get; set; }
        public DateTime? MondayTo { get; set; }
        public DateTime? TuesdayFrom { get; set; }
        public DateTime? TuesdayTo { get; set; }
        public DateTime? WednesdayFrom { get; set; }
        public DateTime? WednesdayTo { get; set; }
        public DateTime? ThursdayFrom { get; set; }
        public DateTime? ThursdayTo { get; set; }
        public DateTime? FridayFrom { get; set; }
        public DateTime? FridayTo { get; set; }
        public DateTime? SaturdayFrom { get; set; }
        public DateTime? SaturdayTo { get; set; }
        public DateTime? SundayFrom { get; set; }
        public DateTime? SundayTo { get; set; }

        public virtual Doctor Doctor { get; set; }

    }
}
