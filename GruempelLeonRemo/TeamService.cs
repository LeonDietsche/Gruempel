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
                name = Console.ReadLine();

                string query = "INSERT INTO TEAMS (NAME, PUNKTE) VALUES ('" + name + "', " + 0 + " )";
                SqlCommand insTeam = new SqlCommand(query, con);

                con.Open();
                insTeam.ExecuteNonQuery();

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
        public List<Team> LoadAllTeams()
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
        public void TeamAusgeben()
        {
            var list = LoadAllTeams();
            foreach(var team in list){
                team.Print();
            }
        }
    }

}
