using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class DoctorImage
    {
        [ForeignKey("Doctor")]
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
