using CodeSampleBackend.ComFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UTController : ApiController
    {
        private BasicCRUD dal;
        public UTController()
        {
            dal = new BasicCRUD();
        }
       

        // GET api/<controller>/5
        public List<UTLog> Get(int fkid,string type)
        {
            return dal.GetEntities<UTLog>(c=>c.FkId== fkid && c.Type==type);
        }

        // POST api/<controller>
        public void Post([FromBody]UTLog value)
        {
            value.LogAt = DateTime.UtcNow;
            dal.Add<UTLog>(value);
        }

    }
}