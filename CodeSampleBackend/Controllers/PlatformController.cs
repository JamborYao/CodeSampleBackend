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
    public class PlatformController : ApiController
    {
        private BasicCRUD dal;
        public PlatformController()
        {
            dal = new BasicCRUD();
        }
        // GET api/<controller>
        public IEnumerable<Platform> Get()
        {
            return dal.GetAll<Platform>();
        }

        // GET api/<controller>/5
        public List<Platform> Get(int id)
        {
            return dal.GetEntities<Platform>(c => c.id == id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        
            List<Platform> platforms = HttpHelper.GetPlatforms();
           
            foreach (var item in platforms)
            {
                dal.AddOrUpdate<Platform>(item, c => c.Name == item.Name, Basic.ToDictionary<Platform>(item));
            }
        }

       
    }
}