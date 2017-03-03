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
        public IHttpActionResult GetIssueViews()
        {
            var paras = ControllerContext.Request.GetQueryNameValuePairs();
            string process = paras.Where(c => c.Key == "process").FirstOrDefault().Value;
            string alias = paras.Where(c => c.Key == "alias").FirstOrDefault().Value;
            string pageStr = paras.Where(c => c.Key == "page").FirstOrDefault().Value;
            string limitStr = paras.Where(c => c.Key == "limit").FirstOrDefault().Value;

            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            var result = context.getIssueView(alias, process).ToList();
            IssuePageView view = new IssuePageView();
            view.Total = result.Count();

            int page = Convert.ToInt32(pageStr);
            int limit = Convert.ToInt32(limitStr);
            if (page != 0 || limit != 0)
            {
                page = page == 0 ? 1 : page;
                result = result.Skip((page - 1) * limit).Take(limit).ToList();
            }
            view.Views = result;
            return Ok(view);
        }

        private List<Issue> QueryIssueView(string pageStr, string limitStr, string alias, string type, out int total)
        {
            var issues = dal.GetAll<Issue>();
            int page = Convert.ToInt32(pageStr);
            int limit = Convert.ToInt32(limitStr);

            IEnumerable<Issue> subIssue = dal.GetAll<Issue>();
            if (alias != null)
            {
                subIssue = from m in subIssue where dal.GetEntities<CodeOwnership>(c => c.Type == "issue" && c.support_alias == alias).Any(x => x.FkId == m.id) == true select m;
            }
            if (type != null)
            {
                subIssue = subIssue.Where(c => (c.Type != null ? c.Type.ToLower().Contains(type.ToLower()) : false));
            }
            total = subIssue.Count();

            if (page != 0 || limit != 0)
            {
                page = page == 0 ? 1 : page;
                subIssue = subIssue.Skip((page - 1) * limit).Take(limit);
            }

            return subIssue.ToList();
           
        }
    }
}