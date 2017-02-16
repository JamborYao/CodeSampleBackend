using CodeSampleBackend.ComFunc;
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
        private BasicCRUD dal;
        public IssueStatusController()
        {
            dal = new BasicCRUD();
        }
       
        // GET: api/IssueStatus
        public IHttpActionResult Get()
        {
            string str = "";
            foreach (var item in dal.GetAll<IssueStatu>())
            {
                str += item.name + ",";
            }
            str = str.Substring(0, str.Length - 1);
            
            return Ok(str);
        }

       

        // POST: api/IssueStatus
        public IHttpActionResult Post([FromBody]IssueStatusLog value)
        {
            dal.AddOrUpdate<IssueStatusLog>(value,c=>c.IssueID==value.IssueID, Basic.ToDictionary<IssueStatusLog>(value));
            return Ok();
        }

       
    }
}
