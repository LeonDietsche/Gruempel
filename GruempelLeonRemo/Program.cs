using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace GruempelLeonRemo
{
    class Program
    {
        private static object teams;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string absolute = Path.GetFullPath(@"Ressources\ASCII.txt");
           var logo = File.ReadAllText(absolute);
            Console.WriteLine(logo);
            Console.ForegroundColor = ConsoleColor.White;
            
            string absolute2 = Path.GetFullPath(@"Ressources\ASCII2.txt");
            var logo2 = File.ReadAllText(absolute2);
            Console.WriteLine(logo2);
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            string absolute3 = Path.GetFullPath(@"Ressources\ASCII3.txt");
            var logo3 = File.ReadAllText(absolute3);
            Console.WriteLine(logo3);
            Console.ForegroundColor = ConsoleColor.White;


            while (true)
            {
                Console.WriteLine();


                Console.WriteLine("Was wollen sie machen? Spieler verwalten (1), Teams verwalten (2) Programm beenden(exit)");
                string Auswahl = Console.ReadLine();
                if (Auswahl == "1")
                {
                    Console.WriteLine("Spielerverwaltung:");
                    Console.WriteLine("Möchten sie einen Spieler erstellen(1), alle Spieler anzeigen(2), einen Spieler suchen(3) oder einen Spieler löschen(4)?");
                    PlayerService person = new PlayerService();
                    TeamService team = new TeamService();
                    string AuswahlSpieler = Console.ReadLine();
                    if (AuswahlSpieler == "1")
                    {
                        person.PersonEinlesen();

                    }

                    else if (AuswahlSpieler == "2")
                    {
                        var playerService = new PlayerService();
                        playerService.PersonenAusgeben();
                    }

                    else if (AuswahlSpieler == "3")
                    {
                        SearchPlayer();
                    }

                    else if (AuswahlSpieler == "4")
                    {
                        var players = SearchPlayer();
                        Console.WriteLine("Choose ID");
                        
                        var id = int.Parse(Console.ReadLine());
                        var player = players.FirstOrDefault(p => p.ID == id);
                        if (player == null)
                        {
                            Console.WriteLine("Kein gültiger Spieler eingegeben.");
                            return;
                        }
                        
                        var con = ConnectionFactory.GetConnection();


                        string deletequery = $@"DELETE FROM PLAYER WHERE ID LIKE '{id}'";
                        string deletequeryaddress = $@"DELETE FROM ADDRESS WHERE ID LIKE '{id}'";
                        var command = new SqlCommand(deletequery, con);
                        var commandaddress = new SqlCommand(deletequeryaddress, con);

                        con.Open();
                        command.ExecuteNonQuery();
                        commandaddress.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine("Spieler gelöscht.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Geben Sie eine der vorgegebenen Funktionen ein");
                        Console.WriteLine("Drücken Sie ENTER um fortzufahren.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.ReadKey();

                }
                else if (Auswahl == "2")
                {
                    Console.WriteLine("Teamverwaltung:");
                    Console.WriteLine("Möchten sie ein Team erstellen(1), alle Teams anzeigen(2), eins löschen(3) oder einen Spieler zuweisen(4)?");
                    var teamService = new TeamService();
                    string AuswahlTeam = Console.ReadLine();
                    if (AuswahlTeam == "1")
                    {
                        teamService.TeamEinlesen();
                    }

                    else if (AuswahlTeam == "2")
                    {
                        teamService.LoadPrintTeams();
                        Console.ReadKey();
                    }

                    else if (AuswahlTeam == "3")
                    {
                        try
                        {
                            teamService.LoadPrintTeams();
                            Console.WriteLine("Choose ID");
                            var teamID = int.Parse(Console.ReadLine()); //Team, welches diese ID hat wird ausgewählt

                            var con = ConnectionFactory.GetConnection();


                            string deletequery = $@"DELETE FROM TEAMS WHERE ID LIKE '{teamID}'";
                            var command = new SqlCommand(deletequery, con);
                            con.Open();
                            command.ExecuteNonQuery();
                            con.Close();
                            Console.WriteLine("Team gelöscht.");
                        }
                        catch (Exception TeamHatSpieler)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Team beeinhaltet noch Spieler.",TeamHatSpieler);
                            Console.ForegroundColor = ConsoleColor.White;


                        }

                    }
                    else if (AuswahlTeam == "4")
                    {
                        var players = SearchPlayer();
                        Console.WriteLine("Choose ID");

                        var id = int.Parse(Console.ReadLine()); //Player, welcher diese ID hat wird ausgewählt
                        var player = players.FirstOrDefault(p => p.ID == id);

                        if(player == null)
                        {
                            Console.WriteLine("Kein gültiger Spieler eingegeben.");
                            return;
                        }


                        var service = new TeamService();
                        var teams = service.LoadPrintTeams();
                        Console.WriteLine("Bitte TeamID wählen");

                        var teamID = int.Parse(Console.ReadLine()); //Team, welches diese ID hat wird ausgewählt

                        var team = teams.FirstOrDefault(t => t.ID == teamID);
                        

                        service.SpielerTeamZuweisen(player, team); ///wird zu Team hinzugefügt

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Geben Sie eine der vorgegebenen Funktionen ein");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }
                else if (Auswahl == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Programm wird beendet.");
                    for (int a = 3; a >= 1; a--)
                    {
                        Console.Write("\n{0}", a);
                        System.Threading.Thread.Sleep(1000);
                    }
                    Console.ForegroundColor = ConsoleColor.White;



                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geben Sie eine der vorgegebenen Funktionen ein");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
                
            }

        private static List<Player> SearchPlayer()
        {
            var playerService = new PlayerService();
            var players = playerService.PersonSuchen();
            foreach (var player in players)
            {
                player.Print();
            }
            return players;
            //Console.WriteLine("Choose Player Id");

            //int a = ConsoleHelper.ReadNumber();
            //var pl = players.FirstOrDefault(p => p.ID == a);

            //pl.Print();
        }
    }
}
