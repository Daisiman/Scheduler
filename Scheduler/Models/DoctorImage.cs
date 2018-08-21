using System.ComponentModel.DataAnnotations.Schema;

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
