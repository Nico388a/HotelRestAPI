using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    /// <summary>
    /// En klasse der er forbundet til ens database, som man kan udnytte til fx at lave metoder
    /// </summary>
    public class ManagerGuest
    {
        /// <summary>
        /// Fobindelse til ens lokale side.
        /// </summary>
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// SQL tekst, der selecter alle gæster
        /// </summary>
        private const string GET_ALL = "Select * from guest";
        /// <summary>
        /// SQL tekst der selecter en gæst 
        /// </summary>
        private const string GET_ONE = "Select * from guest Where Guest_No = @Id";
        /// <summary>
        /// SQL tekst der indsætter en gæst
        /// </summary>
        private const string INSERT = "Insert into guest values(@Id, @Name, @Address)";
        /// <summary>
        /// SQL tekst der opdatere en gæsts indformationer
        /// </summary>
        private const string UPDATE = "Update guest set Guest_No = @Guestid, Name = @Name, Address = @Address where Guest_No = @Id";
        
        /// <summary>
        /// SQL tekst der sletter en gæst
        /// </summary>
        private const string DELETE = "Delete from guest where Guest_No =@Id";

        /// <summary>
        /// Metode der retunere listen alle gæster
        /// </summary>
        /// <returns>alle gæster</returns>
        public IEnumerable<Guest> Get()
        {
            List<Guest> liste = new List<Guest>();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Guest guest = readGuest(reader);
                liste.Add(guest);
            }

            conn.Close();
            return liste;
        }

        /// <summary>
        /// Metode der læser en gæsts indformationer
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>gæsts information</returns>
        private Guest readGuest(SqlDataReader reader)
        {
            Guest guest = new Guest();
            guest.Id = reader.GetInt32(0);
            guest.Name = reader.GetString(1);
            guest.Address = reader.GetString(2);
            return guest;
        }
        /// <summary>
        /// Metode der retunere en enkel gæst
        /// </summary>
        /// <param name="id"></param>
        /// <returns>en gæst</returns>
        public Guest Get(int id)
        {
            Guest guest = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                guest = readGuest(reader);
            }

            conn.Close();
            return guest;
        }

        /// <summary>
        /// Metode der skaber en ny gæst
        /// </summary>
        /// <param name="guest"></param>
        /// <returns>ny gæst</returns>
        public bool Post(Guest guest)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(INSERT, conn);

            cmd.Parameters.AddWithValue("@Id", guest.Id);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Address", guest.Address);
            try
            {
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                ok = noOfRowsAffected == 1 ? true : false;
            }
            catch (SqlException sqlException)
            {
                ok = false;
            }
            finally
            {
                conn.Close();
            }

            return ok;
        }

        /// <summary>
        /// Metode der redigerer en gæsts information
        /// </summary>
        /// <param name="id">gæsts Nr</param>
        /// <param name="guest">gæsts navn</param>
        /// <returns>ny information</returns>
        public bool Put(int id, Guest guest)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@GuestId", guest.Id);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Address", guest.Address);
            cmd.Parameters.AddWithValue("@Id", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }

        /// <summary>
        /// Metode der sletter en gæsts
        /// </summary>
        /// <param name="id">gæst nr</param>
        /// <returns>gæst slettet</returns>
        public bool Delete(int id)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }
    }
}