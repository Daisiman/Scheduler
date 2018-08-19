using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class LiveChat
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
