﻿using CodeSampleBackend.ComFunc;
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
        private DAL.DALProcessLog dal;
        public IssueStatusController()
        {
            dal = new DAL.DALProcessLog();
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

        // GET: api/IssueStatus/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/IssueStatus
        public IHttpActionResult Post([FromBody]IssueStatusLog value)
        {
            dal.AddOrUpdate<IssueStatusLog>(value,c=>c.IssueID==value.IssueID, Basic.ToDictionary<IssueStatusLog>(value));
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
