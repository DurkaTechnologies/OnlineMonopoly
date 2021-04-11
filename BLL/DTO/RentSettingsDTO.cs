using System.Collections.Generic;

namespace BLL.DTO
{
	public class RentSettingDTO
    {
        public int Id { get; set; }

        public int StartCost { get; set; }

        public float FirstCoef { get; set; }

        public int SecondCoef { get; set; }

        public bool ByBranchQuantity { get; set; }

        public virtual ICollection<BranchDTO> Branches { get; set; }

    }
}
