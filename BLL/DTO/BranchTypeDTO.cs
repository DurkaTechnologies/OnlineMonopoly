using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
	public class BranchTypeDTO
	{

        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<BranchDTO> Branches { get; set; }
    }
}
