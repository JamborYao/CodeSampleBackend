using CodeSampleBackend.ComFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeSampleBackend.Controllers
{
    public class HomeController : Controller
    {
        private DAL.DALProcessLog dal;
        public HomeController()
        {
            dal = new DAL.DALProcessLog();
        }
        public ActionResult Index()
        {
            HttpHelper.GetPlatforms();
           // GitHubHelper.GetGitHubCommitEntity(GitHubHelper.GetGitHubJsonObject("https://github.com/Azure-Samples/active-directory-dotnet-graphapi-console", "commits"));
           //DateTime d2 = DateTime.Parse("2016-11-23T21:50:46Z", null, System.Globalization.DateTimeStyles.RoundtripKind);
           //GitHubHelper.GetGitHubJsonObject("https://github.com/Azure-Samples/active-directory-dotnet-graphapi-console","commits");
           // HttpHelper.ConvertBodyToCode(HttpHelper.GetHttpRequestBody(string.Format(Constants.SampleCodeURL,20)));
           //HttpHelper.GetGitHubURL("/en-us/resources/samples/active-directory-dotnet-graphapi-console/");
            ViewBag.Title = "Home Page";
            var test = string.Format(Constants.SampleCodeURL, 11);
            return View();
        }
    }
}
