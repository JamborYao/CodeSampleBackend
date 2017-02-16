using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CodeSampleBackend;
using System.Web.Http.Cors;
using CodeSampleBackend.Models;
using CodeSampleBackend.ComFunc;

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IssueViewsController : ApiController
    {
        private BasicCRUD dal;
        public IssueViewsController()
        {
            dal = new BasicCRUD();
        }


        // GET: api/IssueViews
        public List<IssueView> GetIssueViews()
        {
            var paras = ControllerContext.Request.GetQueryNameValuePairs();
            var idList= paras.Where(c => c.Key == "id").FirstOrDefault();
            //string id, taken;
            //if (idList != null)
            //{
            //    id = idList.Value;
            //}
            //string taken = paras.Where(c => c.Key == "taken").FirstOrDefault().Value;
            return Basic.ConvertIssueToIssueView(dal.GetAll<Issue>(), dal);
        }

       
       
    }
}