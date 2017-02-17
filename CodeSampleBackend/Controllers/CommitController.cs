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



    }
}