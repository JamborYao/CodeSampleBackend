using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;

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
        public HttpStatusCodeResult Post([FromBody]IssueStatusView value)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.Exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(" | ", errors));
            }
            IssueStatusLog log = new IssueStatusLog();
            log.IssueID = value.IssueID;
            log.IssueStatusID = dal.GetEntities<IssueStatu>(c=>c.name==value.IssueStatusName).FirstOrDefault().id;
            log.LogAt = DateTime.UtcNow;
            dal.AddOrUpdate<IssueStatusLog>(log, c=>c.IssueID==value.IssueID, Basic.ToDictionary<IssueStatusLog>(log));
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

       
    }
}
