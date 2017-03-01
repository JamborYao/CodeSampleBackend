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

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommitController : ApiController
    {
        private BasicCRUD dal;
        public CommitController()
        {
            dal = new BasicCRUD();
        }
        public MoonCakeCodeSampleEntities context;
        
        /// <summary>
        /// sync commit to database
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public IHttpActionResult Post()
        {

            var codes = dal.GetAll<Code>();
            foreach (var item in codes)
            {
                GitAPI gitApi = new GitAPI();
                gitApi.GetGitHubCommitObject(item.GitHubUrl).ConvertToCommit(gitApi.CommitBody, item.id).InsertToDatabase(gitApi.Commits,dal,item.GitHubUrl);
            }
            return Ok();
        }

        public IHttpActionResult Get()
        {
            var paras = ControllerContext.Request.GetQueryNameValuePairs();
            string alias = paras.Where(c => c.Key == "alias").FirstOrDefault().Value;
            string process = paras.Where(c => c.Key == "process").FirstOrDefault().Value;
            context = new MoonCakeCodeSampleEntities();
            var result = context.getCodeView(alias, process).ToList();
            CommitPageView view = new CommitPageView();
            view.Total = result.Count();
            string pageStr = paras.Where(c => c.Key == "page").FirstOrDefault().Value;
            string limitStr = paras.Where(c => c.Key == "limit").FirstOrDefault().Value;
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

    }
}