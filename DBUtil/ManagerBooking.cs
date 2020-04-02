using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManagerBooking
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * from booking";
        private const string GET_ONE = "select * from bokking where Booking_id = @Id";
        private const string INSERT = "Insert into Booking values(@BookingNo, @Guest, @Hotel, @DateFrom, @DateTo, @Room)";
        private const string UPDATE = "";
        private const string DELETE = "delete from booking where bookingid = @Id";



        public IEnumerable<Booking> Get()
        {
            List<Booking> liste = new List<Booking>();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Booking booking = readBooking(reader);
                liste.Add(booking);
            }

            conn.Close();
            return liste;
        }

        private Booking readBooking(SqlDataReader reader)
        {
            Booking booking = new Booking();
            booking.BookingNo = reader.GetInt32(0);
            
            ManageHotel managerHotel = new ManageHotel();
            Hotel hotel = managerHotel.Get(reader.GetInt32(1));
            booking.Hotel = hotel;

            ManagerGuest guestManager = new ManagerGuest();
            Guest guest = guestManager.Get(reader.GetInt32(2));
            booking.Guest = guest;

            booking.DateFrom = reader.GetDateTime(3);
            booking.DateTo = reader.GetDateTime(4);
            
            ManagerRoom room = new ManagerRoom();
            booking.Room = room.Get(reader.GetInt32(5), reader.GetInt32(1));
            return booking;
        }

        public Booking Get(int id)
        {
            Booking booking = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                booking = readBooking(reader);
            }

            conn.Close();
            return booking;
        }

        public bool Post(Booking booking)
        {
            bool ok = false;
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(INSERT, conn);
                cmd.Parameters.AddWithValue("@Booking_No", booking.BookingNo);
                cmd.Parameters.AddWithValue("@Hotel", booking.Hotel.Id);
                cmd.Parameters.AddWithValue("@Guest", booking.Guest.Id);
                cmd.Parameters.AddWithValue("@DateFrom", booking.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", booking.DateTo);
                cmd.Parameters.AddWithValue("@Room", booking.Room.RoomNo);

                int noOfRowsAffected = cmd.ExecuteNonQuery();

                ok = noOfRowsAffected == 1 ? true : false;

                return ok;
            }
        }

        //public bool Put(int id)
        //{
            //bool ok = false;

            //SqlConnection conn = new SqlConnection();

        //}

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