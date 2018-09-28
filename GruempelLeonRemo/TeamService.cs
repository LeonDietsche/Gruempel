using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    class TeamService
    {
        
        public void TeamEinlesen()
        {

            var con = ConnectionFactory.GetConnection();
            
            string name;
            string punkte;
            string teamID = string.Empty;

            try
            {
                

                Console.WriteLine("\n Enter Your Teamname");
                name = Console.ReadLine(); ///einlesen und abspeichern in der variable name

                string query = "INSERT INTO TEAMS (NAME, PUNKTE) VALUES ('" + name + "', " + 0 + " )"; ///query/abfrage in variable gespeichert.
                SqlCommand insTeam = new SqlCommand(query, con); ///SqlCommand (insteam) wird erstelllt mit der die query von Oben erfüllt und die Verbindung zur DB hat.

                con.Open();
                insTeam.ExecuteNonQuery(); ///insteam wird ausgeführt

                con.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            Console.ReadLine();
        }

        public void SpielerTeamZuweisen(Player spieler, Team team)
        {
            var query = @"UPDATE dbo.PLAYER
                        SET ID_TEAMS = @teamID
                        WHERE ID = @spielerID;";
            var con = ConnectionFactory.GetConnection();
            var updateCommand = new SqlCommand(query, con);
            updateCommand.Parameters.AddWithValue("@teamID", team.ID);
            updateCommand.Parameters.AddWithValue("@spielerID", spieler.ID);
            con.Open();
            updateCommand.ExecuteNonQuery();
            con.Close();

            spieler.Team = team;

        }
        public List<Team> LoadAllTeams() ///liste mit allen Team erstellen
        {
            var con = ConnectionFactory.GetConnection();
            var query = "SELECT ID, NAME, PUNKTE FROM TEAMS";

            SqlCommand view = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = view.ExecuteReader();
            var teamList = new List<Team>();
            while (dr.Read())
            {
                var team = new Team
                {
                    ID = (int)dr.GetValue(0),
                    Name = (string)dr.GetValue(1),
                    Punkte = (int)dr.GetValue(2)
                };
                teamList.Add(team);
                
            }
            return teamList;
        }
        public List<Team> LoadPrintTeams() ///Alle Teams ausgeben
        {
            var list = LoadAllTeams();
            foreach(var team in list){
                team.Print();
            }
            return list;

        }
        //public void PunkteHinzufügen()
        //{
            
        //    var teams = LoadPrintTeams();
        //    Console.WriteLine("Welchem Team wollen Sie Punkte hinzufügen?");
            

        //    var PunkteEingabe = int.Parse(Console.ReadLine()); //Team, welches diese ID hat wird ausgewählt

        //    var punkte = teams.FirstOrDefault(t => t.ID == PunkteEingabe); ///Gibt erstes Element zurück welches die Bedingung erfüllt

        //    Console.WriteLine("Anzahl Punkte Gewonnen (2), Gleichstand(1), Verloren(0)");
        //    string Punkte = Console.ReadLine();
        //    string querypunkte = "INSERT INTO PUNKTE FROM TEAMS WHERE ID = punkte VALUES(" + Punkte + ")";
        /// UPdate sript not insert
        //}
    }

}
