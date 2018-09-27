using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    public class Spiel
    {
        public DateTime StartSpiel { get; set; }
        public TimeSpan Duration { get; } = TimeSpan.FromMinutes(30);
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }
        public Spiel(Team teamA, Team teamB)
        {
            TeamA = teamA;
            TeamB = teamB;
        }
        
        internal void Print()
        {
            Console.WriteLine($"Start: {StartSpiel.ToString("dd. MMM. H:mm")}\tTeamA: {TeamA.Name}\tTeamB: {TeamB.Name} ");


        }

    }
}
