using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
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
    public class ProcessController : ApiController
    {
        public ProcessController()
        {
            dal = new BasicCRUD();
        }
        private BasicCRUD dal;
        // GET api/<controller>
        public string Get()
        {
            string str = "";
            foreach (var item in dal.GetAll<Process>())
            {
                str += item.name + ",";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var result = dal.GetEntities<Process>(c => c.id == id);
            return Ok(result);
        }

        // POST api/<controller>
        public HttpStatusCodeResult Post([FromBody]ProcessView value)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.Exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(" | ", errors));
            }
            ProcessLog log = new ProcessLog();
            log.Type = value.Type;
            log.ProcessID = dal.GetEntities<Process>(c => c.name == value.ProcessName).FirstOrDefault().id;
            log.FkId = value.FkId;
            log.LogAT = DateTime.UtcNow;
            dal.Add<ProcessLog>(log);
            return new HttpStatusCodeResult(HttpStatusCode.OK);


        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            dal.Delete<Process>(dal.GetEntities<Process>(c => c.id == id));
        }
    }
}