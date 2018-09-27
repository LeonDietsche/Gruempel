using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    public class SpielplanCreator
    {
        public void SpielplanAusgabe()
        {
            TeamService teamService = new TeamService();
            var teams = teamService.LoadAllTeams();
           
                 
            List<Spiel> SpielList = new List<Spiel>();

            for (int i = 0; i < teams.Count; i++)
            {
               

                var TeamA = teams[i];

                for (int j = i+1; j < teams.Count; j++)
                {
                    var TeamB = teams[j];
                    Spiel spiel = new Spiel(TeamA, TeamB);
                    SpielList.Add(spiel);
                }
               
            }
            var random = new Random();
            SpielList = SpielList.OrderBy((item) => {
                return random.Next();
                    }).ToList();
            var StartZeit = DateTime.Now;
            foreach (var spiel in SpielList)
            {
                spiel.StartSpiel = StartZeit;
                StartZeit = StartZeit.Add(spiel.Duration);

                spiel.Print();
            }

        }
    }
}
