using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
   public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Punkte { get; set; }

        internal void Print()
        {
            Console.WriteLine($"ID: {ID}\tName: {Name}\tPunkte: {Punkte}");
            ConsoleHelper.PrintSeperator();
            
        }
    }

}
