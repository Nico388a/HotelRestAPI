using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManagerGeneric
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const string GET_ALL = "Select * from guest";
        private const string GET_ONE = "Select * from guest Where Guest_No = @Id";
        private const string INSERT = "Insert into guest values(@Id, @Name, @Address)";
        private const string UPDATE =
            "Update guest set Guest_No = @Guestid, Name = @Name, Address = @Address where Guest_No = @Id";

        private const string DELETE = "Delete from guest where Guest_No =@Id";

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

        private Guest readGuest(SqlDataReader reader)
        {
            Guest guest = new Guest();
            guest.Id = reader.GetInt32(0);
            guest.Name = reader.GetString(1);
            guest.Address = reader.GetString(2);
            return guest;
        }

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


        public bool Post(Guest guest)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(INSERT, conn);

            cmd.Parameters.AddWithValue("@Id", guest.Id);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Address", guest.Address);
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }


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