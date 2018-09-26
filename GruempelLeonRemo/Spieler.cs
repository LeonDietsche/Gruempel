using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    public class Spieler
    {
        public int ID { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string Telefonnummer { get; set; }
        public Address Address { get; set; }
        public Team Team { get; set; }

        public void Print()
        {
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Vorname: {Vorname}");
            Console.WriteLine($"Nachname: {Nachname}");
            Console.WriteLine($"Telefonnummer: {Telefonnummer}");
            


            Address.Print();
        }
    }
}

