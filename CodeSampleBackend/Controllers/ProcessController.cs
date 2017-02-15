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
    public class ProcessController : ApiController
    {
        public ProcessController()
        {
            dal = new DAL.DALProcessLog();
        }
        private DAL.DALProcessLog dal;
        // GET api/<controller>
        public string Get()
        {
            string str = "";
            foreach (var item in dal.GetAll<Process>())
            {
                str +=  item.name+",";
            }
            str = str.Substring(0,str.Length-1) ;
            return str;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var result = dal.GetEntities<Process>(c=>c.id==id);
            return Ok(result);
        }

        // POST api/<controller>
        public void Post([FromBody]ProcessLog value)
        {
            dal.Add<ProcessLog>(value);
        }
        
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            dal.Delete<Process>(dal.GetEntities<Process>(c=>c.id==id));
        }
    }
}