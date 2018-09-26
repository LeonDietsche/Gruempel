using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    public class Address
    {
        public int ID { get; set; }

        public string Strasse { get; set; }

        public string HausNr { get; set; }

        public string PLZ { get; set; }

        public string Ort { get; set; }

        internal void Print()
        {
            Console.WriteLine($"Strasse: {Strasse}");
            Console.WriteLine($"HausNr: {HausNr}");
            Console.WriteLine($"PLZ: {PLZ}");
            Console.WriteLine($"Ort: {Ort}");
        }

        
    }
}
