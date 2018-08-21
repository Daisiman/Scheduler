using System;
using System.ComponentModel.DataAnnotations;

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
