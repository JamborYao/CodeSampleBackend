using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IssueStatusController : ApiController
    {
        [ResponseType(typeof(IssueStatu))]
        // GET: api/IssueStatus
        public IHttpActionResult Get()
        {
            return Ok(DAL.DALProcessLog.GetAllIssueStatus());
        }

        // GET: api/IssueStatus/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/IssueStatus
        public IHttpActionResult Post([FromBody]IssueStatusLog value)
        {
            DAL.DALIssueStatusLog.AddIssueStatus(value);
            return Ok();
        }

        // PUT: api/IssueStatus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/IssueStatus/5
        public void Delete(int id)
        {
        }
    }
}
