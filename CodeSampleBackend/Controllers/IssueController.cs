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
        public MoonCakeCodeSampleEntities context;
        
        // GET api/<controller>
        public List<Issue> Get()
        {
            context = new MoonCakeCodeSampleEntities();
            var entities = context.Issues;
            if (entities != null)
            {
                return entities.ToList<Issue>();
            }
            else {
                return null;
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

    
    

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

            var codes = DAL.DALCode.GetAllCode();
            foreach (var item in codes)
            {
                List<Issue> commits =DAL.DALIssue. GetGitHubIssueEntity(GitHubHelper.GetGitHubIssueObject(item.GitHubUrl), item.id);
              
                DAL.DALIssue.AddCommitsIfNotExistedElseUpdate(commits);
            }

        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}