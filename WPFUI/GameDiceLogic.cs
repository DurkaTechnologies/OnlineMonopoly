using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI
{
    public class GameDiceLogic
    {
        public int FirstValue { get; private set; }
        public int SecondValue { get; private set; }
        const int MIN = 1;
        const int MAX = 1;

        public void Action()
        {
            Random rand = new Random();
            this.FirstValue = rand.Next(MIN, MAX);
            this.SecondValue = rand.Next(MIN, MAX);
        }
    }
}
