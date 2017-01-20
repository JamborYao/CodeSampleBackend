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
        // GET api/<controller>
        public string Get()
        {
            string str = "";
            foreach (var item in DAL.DALProcessLog.GetAllProcess())
            {
                str +=  item.name+",";
            }
            str = str.Substring(0,str.Length-1) ;
            return str;
        }

        // GET api/<controller>/5
        public Process[] Get(int id)
        {
           
            var processes =  DAL.DALProcessLog.GetAllProcess();

            return processes.ToArray();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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