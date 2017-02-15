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

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IssueViewsController : ApiController
    {
        private MoonCakeCodeSampleEntities db = new MoonCakeCodeSampleEntities();

        // GET: api/IssueViews
        public List<IssueView> GetIssueViews()
        {
            return DAL.DALIssueView.GetAllIssueView();
        }

       
       
    }
}