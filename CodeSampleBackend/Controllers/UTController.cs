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
        private DAL.DALProcessLog dal;
        public UTController()
        {
            dal = new DAL.DALProcessLog();
        }
       

        // GET api/<controller>/5
        public List<UTLog> Get(int id,string type)
        {
            return dal.GetEntities<UTLog>(c=>c.FkId==id&&c.Type==type);
        }

        // POST api/<controller>
        public void Post([FromBody]UTLog value)
        {
            dal.Add<UTLog>(value);
        }

    }
}