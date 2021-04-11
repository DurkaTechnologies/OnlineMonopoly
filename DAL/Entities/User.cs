using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
	public class User
    {
		public User()
		{
            Friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public string Image { get; set; }

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

        public virtual ICollection<User> Friends { get; set; }
    }
}
