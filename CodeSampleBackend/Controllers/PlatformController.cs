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
        // GET api/<controller>
        public IEnumerable<Platform> Get()
        {
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            return context.Platforms;
        }

        // GET api/<controller>/5
        public Platform Get(int id)
        {
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();

            return context.Platforms.Where(c => c.id == id).FirstOrDefault();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            List<Platform> platforms = HttpHelper.GetPlatforms();
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            foreach (var item in platforms)
            {

                if (context.Platforms.Any(c => c.Name == item.Name))
                {
                    var platform = context.Platforms.Where(c => c.Name == item.Name).FirstOrDefault();
                    platform.Value = item.Value;
                    context.Entry<Platform>(platform).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    context.Platforms.Attach(item);
                    context.Entry<Platform>(item).State = System.Data.Entity.EntityState.Added;
                }
                context.SaveChanges();

            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}