using CodeSampleBackend.ComFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

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
        public HttpStatusCodeResult Post([FromBody]UTLog value)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.Exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(" | ", errors));
            }
            value.LogAt = DateTime.UtcNow;
            dal.Add<UTLog>(value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}