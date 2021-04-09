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
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public BranchType BranchType { get; set; }

        public string Image { get; set; }

        [Required]
        public int Pledge { get; set; }

        [Required]
        public int Buyout { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Upgrade { get; set; }

        public virtual RentSetting RentSetting { get; set; }
    }
}
