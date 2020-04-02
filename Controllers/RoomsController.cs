using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelRestAPI.DBUtil;
using ModelLibrary;

namespace HotelRestAPI.Controllers
{
    public class RoomsController : ApiController
    {
         private static ManagerRoom manager = new ManagerRoom();
        // GET: api/Rooms
        public IEnumerable<Room> Get()
        {
            return manager.Get();
        }

        // GET: api/Rooms/5
        [HttpGet]
        [Route("api/Rooms/{id}/{hotelNo}")]
        public Room Get(int id,int hotelNo)
        {
            return manager.Get(id, hotelNo);
        }

        // POST: api/Rooms
        public bool Post(Room room)
        {
            return manager.Post(room);
        }

        // PUT: api/Rooms/5
        [HttpPut]
        [Route("api/Rooms/{id}/{hotelNo}")]
        public bool Put(int id, int hotelNo, Room room)
        {
            return manager.Put( id, hotelNo, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete]
        [Route("api/Rooms/{id}/{hotelNo}")]
        public bool Delete(int id, int hotelNo)
        {
            return manager.Delete(id, hotelNo);
        }
    }
}
