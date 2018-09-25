using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    class Teams
    {
        public void spielerhinzufuegen()
        {

        }
        public void TeamEinlesen()
        {

            SqlConnection con;
            string str;
            string name;
            string punkte;
            string teamID = string.Empty;

            try
            {
                str = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = GruempelDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
                con = new SqlConnection(str);
                con.Open();

                Console.WriteLine("\n Enter Your Teamname");
                name = Console.ReadLine();

                string query = "INSERT INTO TEAMS (NAME, PUNKTE) VALUES ('" + name + "', " + 0 + " )";
                SqlCommand insTeam = new SqlCommand(query, con);
                //int ID = Convert.ToInt32(insTeam.ExecuteScalar());
                
                insTeam.ExecuteNonQuery();

                con.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            Console.ReadLine();
        }
    }
}
