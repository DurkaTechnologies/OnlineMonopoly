using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI
{
    public class GameDiceLogic
    {
        Random rand;

        public int FirstValue { get; private set; }
        public int SecondValue { get; private set; }
        const int MIN = 0;
        const int MAX = 6;

		public GameDiceLogic()
		{
            rand = new Random(DateTime.Now.Millisecond);
        }

        public void Action()
        {
            this.FirstValue = rand.Next(MIN, MAX);
            this.SecondValue = rand.Next(MIN, MAX);
        }
    }
}
