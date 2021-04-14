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
        [Key]
        public int Id { get; set; }

        [Required]
        public int StartRent { get; set; }

        public int FirstLevel { get; set; }
        public int SecondLevel { get; set; }
        public int ThirdLevel { get; set; }
        public int FourthLevel { get; set; }
        public int FifthLevel { get; set; }

        [Required]
        public bool ByBranchQuantity { get; set; }
    }
}
