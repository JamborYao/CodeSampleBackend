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
    public class CodeOwnerController : ApiController
    {
        private BasicCRUD dal;
        public CodeOwnerController()
        {
            dal = new BasicCRUD();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int fkid, string type)
        {
            var entities = dal.GetEntities<CodeOwnership>(c => c.FkId == fkid && c.Type == type).OrderByDescending(c => c.LogAt).FirstOrDefault();
            if (entities != null)
            {
                return Ok(entities.support_alias);
            }
            else
            {
                return null;
            }
        }

        // POST api/<controller>
        //public HttpStatusCodeResult Post([FromBody]CodeOwnership value)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.Exception);
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(" | ", errors));
        //    }
        //    value.LogAt = DateTime.UtcNow;
        //    dal.AddOrUpdate<CodeOwnership>(value, c => c.FkId == value.FkId && c.Type == value.Type, Basic.ToDictionary<CodeOwnership>(value));
        //    return new HttpStatusCodeResult(HttpStatusCode.OK);

        //}
        public CodeOwnership Post([FromBody]CodeOwnership value)
        {
            dal.AddOrUpdate<CodeOwnership>(value, c => c.FkId == value.FkId && c.Type == value.Type, Basic.ToDictionary<CodeOwnership>(value));
            return value;
        }


    }
}