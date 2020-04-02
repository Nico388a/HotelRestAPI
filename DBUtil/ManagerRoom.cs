using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManagerRoom
    {
        private const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const string GET_ALL = "select * from room";
        private const string GET_ONE = "select * from room where Room_No = @RoomNo and Hotel_No = @HotelNo";
        private const string INSERT = "Insert into room values(@RoomNo, @HotelNo, @Types, @Price)";
        private const string UPDATE = "update room set Room_No = @RoomNo, Hotel_No = @HotelNo, Types = @Types, Price =@Price where Room_No = @Id and Hotel_No = @IdHotel";
        private const string DELETE = "delete from room where Room_No = @Id and Hotel_No = @IdHotel";

        public IEnumerable<Room> Get()
        {
            List<Room> liste = new List<Room>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Room room = readRoom(reader);
                liste.Add(room);
            }

            conn.Close();
            return liste;
        }
        private Room readRoom(SqlDataReader reader)
        {
            Room room = new Room();
            room.RoomNo = reader.GetInt32(0);
            room.HotelNo = reader.GetInt32(1);
            room.Types = reader.GetString(2);
            room.Price = reader.GetDouble(3);
            return room;
        }

        public Room Get(int id, int hotelNo)
        {
            Room room = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@RoomNo", id);
            cmd.Parameters.AddWithValue("@HotelNo", hotelNo);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                room = readRoom(reader);
            }
            conn.Close();
            return room;
        }

        public bool Post(Room room)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(INSERT, conn);

            cmd.Parameters.AddWithValue("@RoomNo", room.RoomNo);
            cmd.Parameters.AddWithValue("@HotelNo", room.HotelNo);
            cmd.Parameters.AddWithValue("@Types", room.Types);
            cmd.Parameters.AddWithValue("@Price", room.Price);
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }

        public bool Put(int id, int hotelNo,  Room room)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@RoomNo", room.RoomNo);
            cmd.Parameters.AddWithValue("@HotelNo", room.HotelNo);
            cmd.Parameters.AddWithValue("@Types", room.Types);
            cmd.Parameters.AddWithValue("@Price", room.Price);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@IdHotel", hotelNo);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }

        public bool Delete(int id, int hotelNo)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@IdHotel", hotelNo);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;
        }
    }

        
    
}



