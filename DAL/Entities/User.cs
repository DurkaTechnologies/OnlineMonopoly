using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_UniqueKeyString", IsUnique = true, Order = 1)]
        [MaxLength(100)]
        public string Login { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        [MaxLength(150)]
        [Index("IX_UniqueKeyString", IsUnique = true, Order = 2)]
        public string Email { get; set; }
        [DefaultValue(0)]
        public int Rating { get; set; }
        [DefaultValue(0)]
        public int Money { get; set; }
    }
}
