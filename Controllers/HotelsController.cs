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
    public class HotelsController : ApiController
    {
        private static IManageHotel manager = new ManageHotel();

        // GET: api/Hotels
        public IEnumerable<Hotel> Get()
        {
            return manager.Get();
        }

        // GET: api/Hotels/5
        public Hotel Get(int id)
        {
            return manager.Get(id);
        }

        // POST: api/Hotels
        public bool Post(Hotel hotel)
        {
            return manager.Post(hotel);
        }

        // PUT: api/Hotels/5
        public bool Put(int id, Hotel hotel)
        {
            return manager.Put(id, hotel);
        }

        // DELETE: api/Hotels/5
        public bool Delete(int id)
        {
            return manager.Delete(id);
        }
    }
}
