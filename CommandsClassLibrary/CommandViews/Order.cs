using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandsClassLibrary.CommandViews
{
	[Serializable]
	public class Order
	{
		public Order()
		{
			Money = 0;
		}

		public Order(List<int> branches, int money)
		{
			Branches = branches;
			Money = money;
		}
		public List<int> Branches { get; set; }

		public int Money { get; set; }
	}
}
