using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
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
    public class IssueController : ApiController
    {
        private DAL.DALProcessLog dal;
        public IssueController()
        {
            dal = new DAL.DALProcessLog();
        }
        public MoonCakeCodeSampleEntities context;





        /// <summary>
        /// sync all issues to database
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public void Post([FromBody]string value)
        {

            var codes = dal.GetAll<Code>();
            foreach (var item in codes)
            {
                List<Issue> issues = DAL.DALIssue.GetGitHubIssueEntity(GitHubHelper.GetGitHubIssueObject(item.GitHubUrl), item.id);

                foreach (var issue in issues)
                {

                    dal.AddOrUpdate<Issue>(issue, c => c.CreateAt == issue.CreateAt, Basic.ToDictionary<Issue>(issue));
                }

            }

        }

    }
}