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
           

            try
            {

                str = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = GruempelDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
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
                ///Eingabe = Console.ReadLine();
                ///double EingabeZah
                

                zip = int.Parse(Console.ReadLine());

                Console.WriteLine("\n Enter Your City");
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
                


                con.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            Console.ReadLine();


        }
        public void ClearTable()
        {
            SqlConnection con;
            string str;

            str = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = GruempelDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
            con = new SqlConnection(str);
            con.Open();  

            string sqlTrunc = "TRUNCATE TABLE PLAYER";
            SqlCommand cmd = new SqlCommand(sqlTrunc, con);
            cmd.ExecuteNonQuery();
            con.Close();
                     Console.ReadKey();
        }
        public void ClearTableAddress()
        {
            SqlConnection con;
            string str;

            str = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = GruempelDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
            con = new SqlConnection(str);
            con.Open();

            string sqlTrunc = "TRUNCATE TABLE ADDRESS";
            SqlCommand cmd = new SqlCommand(sqlTrunc, con);
            cmd.ExecuteNonQuery();
            con.Close();
            Console.ReadKey();
        }

        public void PersonenAusgeben()
        {
            SqlConnection con;
            string str;

            str = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = GruempelDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
            con = new SqlConnection(str);
            con.Open();

            string q = "SELECT * FROM PLAYER INNER JOIN ADDRESS ON PLAYER.ID_ADDRESS = ADDRESS.ID";
            
            SqlCommand view = new SqlCommand(q, con);

            SqlDataReader dr = view.ExecuteReader();
            while (dr.Read())
            {
                
                Console.WriteLine("Vorname :" + dr.GetValue(1).ToString());
                Console.WriteLine("Nachname :" + dr.GetValue(2).ToString());
                Console.WriteLine("Telefonnummer :" + dr.GetValue(3).ToString());
                Console.WriteLine("Strasse :" + dr.GetValue(5).ToString());
                
                Console.ReadKey();
            }
            con.Close();


        }

       
    }
}
