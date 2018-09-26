using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    class PlayerService ///Person
    {
        
        public void PersonEinlesen()
        {
            SqlConnection con;
            string str;
            string nachname;
            string vorname;
            string telefonnummer;
            string street;
            string housenumber;
            int zip;
            string city;
            string addressID = string.Empty;
           

            try
            {

                con = ConnectionFactory.GetConnection();
                con.Open();

                Console.WriteLine("Database connected");
                Console.WriteLine("\n Bitte Vorname eingeben");
                vorname = Console.ReadLine();

                Console.WriteLine("\n Bitte Nachname eingeben");
                nachname = Console.ReadLine();
                Console.WriteLine("\n Bitte Telefonnummer eingeben");
                telefonnummer = Console.ReadLine();

                Console.WriteLine("\n Bitte Strasse eingeben");
                street = Console.ReadLine();

                Console.WriteLine("\n Bitte Hausnummer eingeben");
                housenumber = Console.ReadLine();

                Console.WriteLine("\n Bitte Postleitzahl eingeben");
                zip = int.Parse(Console.ReadLine());

                Console.WriteLine("\n Bitte Ort eingeben");
                city = Console.ReadLine();
                
                string query1 = "INSERT INTO ADDRESS (STREET, HOUSENUMBER, ZIP, CITY) VALUES ('" + street + "', '" + housenumber + "'," + zip + ", '" + city + "'); SELECT SCOPE_IDENTITY();";
                SqlCommand ins1 = new SqlCommand(query1, con);
                int ID = Convert.ToInt32(ins1.ExecuteScalar());
               

                string query = "INSERT INTO PLAYER (VORNAME, NACHNAME,TELEFONNUMMER, ID_ADDRESS) VALUES ('" + vorname + "', '" + nachname + "', '" + telefonnummer + "' , " + ID + " )";
                SqlCommand ins = new SqlCommand(query, con);
                ins.ExecuteNonQuery();

                Console.WriteLine("\n Data stored into Database");
                string q = "SELECT * FROM PLAYER INNER JOIN ADDRESS ON PLAYER.ID_ADDRESS = ADDRESS.ID";
                SqlCommand view = new SqlCommand(q, con);
                SqlDataReader dr = view.ExecuteReader();


                
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            Console.ReadLine();


        }
        

        public List<Spieler> LoadAllPlayers()
        {
            var con = ConnectionFactory.GetConnection();

            string q = @"SELECT P.ID, P.VORNAME, P.NACHNAME, P.TELEFONNUMMER, A.STREET, A.HOUSENUMBER, A.ZIP, A.CITY 
                        FROM PLAYER AS P INNER JOIN ADDRESS AS A ON P.ID_ADDRESS = A.ID";


            SqlCommand view = new SqlCommand(q, con);

            SqlDataReader dr = view.ExecuteReader();
            var playerList = new List<Spieler>();
            while (dr.Read())
            {
                var player = new Spieler
                {
                    ID = (int)dr.GetValue(0),
                    Vorname = (string)dr.GetValue(1),
                    Nachname = (string)dr.GetValue(2),
                    Telefonnummer = (string)dr.GetValue(3),
                    Address = new Address
                    {
                        Strasse = (string)dr.GetValue(4),
                        HausNr = (string)dr.GetValue(5),
                        PLZ = (string)dr.GetValue(6),
                        Ort = (string)dr.GetValue(7)
                    }

                };
                playerList.Add(player);
                //Console.WriteLine("ID: " + dr.GetValue(0));
                //Console.WriteLine("Vorname: " + dr.GetValue(1).ToString());
                //Console.WriteLine("Nachname: " + dr.GetValue(2).ToString());
                //Console.WriteLine("Telefonnummer: " + dr.GetValue(3).ToString());
                //Console.WriteLine("Strasse: " + dr.GetValue(4).ToString());
                //Console.WriteLine("HausNr: " + dr.GetValue(5).ToString());
                //Console.WriteLine("PLZ: " + dr.GetValue(6).ToString());
                //Console.WriteLine("Ort: " + dr.GetValue(7).ToString());
                //ConsoleHelper.PrintSeperator();

            }
            con.Close();
            return playerList;

        }


        public void PersonenAusgeben()
        {
            
            var players = LoadAllPlayers();
            foreach(var player in players)
            {
                player.Print();
                ConsoleHelper.PrintSeperator();
            }
            Console.WriteLine("Bitte beliebige Taste drücken.");
            Console.ReadKey();

            
        }
        //public void TeamZuweisen(Spieler.)
        //{

        //}

       
    }
}
