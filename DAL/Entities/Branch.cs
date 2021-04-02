using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_UniqueKeyString", IsUnique = true, Order = 1)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        public int pledge { get; set; }
        [Required]
        public int Buyout { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Upgrade { get; set; }
        public virtual RentSetting RentSetting { get; set; }

    }
}
