using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI
{
    public class GameDiceLogic
    {
		private const int MIN = 0;
		private const int MAX = 6;
		private Random random;

		public GameDiceLogic()
		{
			random = new Random(DateTime.Now.Millisecond);
		}

        public int FirstValue { get; private set; }
        public int SecondValue { get; private set; }

        public void Action()
        {
            FirstValue = random.Next(MIN, MAX);
            SecondValue = random.Next(MIN, MAX);
        }
    }
}
