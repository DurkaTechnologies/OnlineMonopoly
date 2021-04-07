using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
	public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [DefaultValue(0)]
        public int Rating { get; set; }

        [DefaultValue(0)]
        public int Money { get; set; }
    }
}
