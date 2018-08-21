using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Models
{
    public class BlackList
    {
        public ApplicationUser User { get; set; }

        [Required]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime DateAdded { get; set; }
    }
}
