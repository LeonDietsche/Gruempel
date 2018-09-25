using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruempelLeonRemo
{
    class PersonsInDB ///Person
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
            
            string teamID;

            try
            {

                str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\diets\source\repos\GruempelLeonRemo\GruempelLeonRemo\Database1.mdf;Integrated Security=True";
                con = new SqlConnection(str);
                con.Open();
                Console.WriteLine("Database connected");
                Console.WriteLine("\n Enter Your Vorname");
                vorname = Console.ReadLine();
                Console.WriteLine("\n Enter Your Nachname");
                nachname = Console.ReadLine();
                Console.WriteLine("\n Enter Your Telefonnummer");
                telefonnummer = Console.ReadLine();

                Console.WriteLine("\n Enter Your Street");
                street = Console.ReadLine();

                Console.WriteLine("\n Enter Your Housenumber");
                housenumber = Console.ReadLine();

                Console.WriteLine("\n Enter Your ZIP");
                zip = int.Parse(Console.ReadLine());

                Console.WriteLine("\n Enter Your City");
                city = Console.ReadLine();

                string query1 = "INSERT INTO ADDRESS (STREET, HOUSENUMBER, ZIP, CITY) VALUES ('" + street + "', '" + housenumber + "'," + zip + ", '" + city + "' )";
                SqlCommand ins1 = new SqlCommand(query1, con);
                ///ins1.ExecuteNonQuery();
                
                SqlDataReader dr1 = ins1.ExecuteReader();
                while (dr1.Read())
                {
                    addressID = dr1.GetValue(0).ToString();
                    Console.WriteLine("\n Street :" + dr1.GetValue(1).ToString());
                    Console.WriteLine("\n Housenumber :" + dr1.GetValue(2).ToString());
                    Console.WriteLine("\n ZIP :" + dr1.GetValue(3).ToString());
                    Console.WriteLine("\n City :" + dr1.GetValue(4).ToString());


                }
                dr1.Close();

                string query = "INSERT INTO PLAYER (VORNAME, NACHNAME,TELEFONNUMMER, ID_ADDRESS) VALUES ('" + vorname + "', '" + nachname + "', '" + telefonnummer + "' , '" + addressID + "')";
                SqlCommand ins = new SqlCommand(query, con);
                ins.ExecuteNonQuery();
                Console.WriteLine("\n Data stored into Database");
                string q = "SELECT * FROM PLAYER";
                SqlCommand view = new SqlCommand(q, con);
                SqlDataReader dr = view.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine("\n Vorname :" + dr.GetValue(1).ToString());
                    Console.WriteLine("\n Nachname :" + dr.GetValue(2).ToString());
                    Console.WriteLine("\n Telefonnummer :" + dr.GetValue(3).ToString());

                }

               
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
