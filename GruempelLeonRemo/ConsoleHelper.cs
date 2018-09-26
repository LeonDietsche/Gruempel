using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    public class ConsoleHelper
    {
        public static void PrintSeperator()
        {
            Console.WriteLine("------------------");
        }

        public static int ReadNumber()
        {
            while (true)
            {
                var value = Console.ReadLine();
                int res = 0;
                if(int.TryParse(value, out res))
                {
                    return res;
                }
                Console.WriteLine("Bitte gültige Zahl eingeben");
            }
        }
    }
}
