using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class DoctorAndWorkHours
    {
        //public Doctor Doctor { get; set; }
        //public DoctorWorkHours DoctorWorkHours { get; set; }
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

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime MondayFrom { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime MondayTo { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime TuesdayFrom { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime TuesdayTo { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime WednesdayFrom { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime WednesdayTo { get; set; }
        //public DateTime ThursdayFrom { get; set; }
        //public DateTime ThursdayTo { get; set; }
        //public DateTime FridayFrom { get; set; }
        //public DateTime FridayTo { get; set; }
        //public DateTime SaturdayFrom { get; set; }
        //public DateTime SaturdayTo { get; set; }
        //public DateTime SundayFrom { get; set; }
        //public DateTime SundayTo { get; set; }
    }
}
