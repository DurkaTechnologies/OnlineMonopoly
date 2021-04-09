using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class RentSetting
    {
        public RentSetting()
        {
            Branches = new HashSet<Branch>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int StartCost { get; set; }

        [Required]
        public float FirstCoef { get; set; }

        [Required]
        public int SecondCoef { get; set; }

        [Required]
        public bool ByBranchQuantity { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
