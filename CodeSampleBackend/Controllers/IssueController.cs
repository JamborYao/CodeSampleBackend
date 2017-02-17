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
        private BasicCRUD dal;
        public IssueController()
        {
            dal = new BasicCRUD();
        }
        public MoonCakeCodeSampleEntities context;





        /// <summary>
        /// sync all issues to database
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public IHttpActionResult Post()
        {

            var codes = dal.GetAll<Code>();
            foreach (var item in codes)
            {
                GitAPI api = new GitAPI();
                api.GetGitHubIssueObject(item.GitHubUrl).ConvertToIssue(api.issueBody, item.id).InsertToDatabase(api.issues,dal);
            }
            return Ok();

        }

    }
}