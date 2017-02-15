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
        private DAL.DALProcessLog dal;
        public CommitController()
        {
            dal = new DAL.DALProcessLog();
        }
        public MoonCakeCodeSampleEntities context;
        
        /// <summary>
        /// sync commit to database
        /// </summary>
        /// <param name="value"></param>
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
           
            var codes =DAL.DALCode.GetAllCode();
            foreach (var item in codes)
            {
                List<Commit> commits =  GitHubHelper.GetGitHubCommitEntity(GitHubHelper.GetGitHubCommitObject(item.GitHubUrl),item.id);
                foreach (var i in commits)
                {
                    i.GitHubUrl = item.GitHubUrl; 
                }
                DAL.DALCommit.AddCommitsIfNotExistedElseUpdate(commits);
            }
          
        }



    }
}