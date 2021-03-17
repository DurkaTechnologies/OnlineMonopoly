using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            MonopolyDbContext context = new MonopolyDbContext();
            foreach (var item in context.Users)
            {

            }
        }
    }
}
