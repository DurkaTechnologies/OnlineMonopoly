using System.Collections.Generic;

namespace BLL.DTO
{
	public class RentSettingDTO
    {
        public int Id { get; set; }

        public int StartRent { get; set; }

        public int FirstLevel { get; set; }
        public int SecondLevel { get; set; }
        public int ThirdLevel { get; set; }
        public int FourthLevel { get; set; }
        public int FifthLevel { get; set; }

        public bool ByBranchQuantity { get; set; }

        public virtual ICollection<BranchDTO> Branches { get; set; }

    }
}
