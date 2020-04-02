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
    public class GuestController : ApiController
    {
        private static ManagerGuest manager = new ManagerGuest();

        // GET: api/Guest
        public IEnumerable<Guest> Get()
        {
            return manager.Get();
        }

        // GET: api/Guests/5
        public Guest Get(int id)
        {
            return manager.Get(id);
        }

        // POST: api/Guests
        public bool Post(Guest guest)
        {
            return manager.Post(guest);
        }

        // PUT: api/Guests/5
        public bool Put(int id, Guest guest)
        {
            return manager.Put(id, guest);
        }

        // DELETE: api/Guest/5
        public bool Delete(int id)
        {
            return manager.Delete(id);
        }

    }
}
