using CodeSampleBackend.ComFunc;
using CodeSampleBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace CodeSampleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AComCodeController : ApiController
    {
        private BasicCRUD dal;
        public AComCodeController()
        {
            dal = new BasicCRUD();
        }

        /// <summary>
        /// test documation
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [ResponseType(typeof(PageCodeView))]
        public IHttpActionResult Get()
        {
            var paras = ControllerContext.Request.GetQueryNameValuePairs();
            string page = paras.Where(c => c.Key == "page").FirstOrDefault().Value;
            string limit = paras.Where(c => c.Key == "limit").FirstOrDefault().Value;
            string product = paras.Where(c => c.Key == "product").FirstOrDefault().Value;
            string platform = paras.Where(c => c.Key == "platform").FirstOrDefault().Value;
            var pageview = DAL.DALGenerateView.GetCodeView(dal.GetAll<Code>(), page, limit,product,platform);
            return Ok(pageview);
        }

       
        public string Post()
        {
            int i = 1;
            while (true)
            {
                List<Code> codes = HttpHelper.ConvertBodyToCode(HttpHelper.GetHttpRequestBody(string.Format(Constants.SampleCodeURL, i)));
                i++;
                if (codes == null)
                {
                    break;
                }
                else
                {
                    MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
                    foreach (var item in codes)
                    {
                        if (context.Codes.Any(c => c.Title == item.Title))
                        {
                            var code = context.Codes.Where(c => c.Title == item.Title).FirstOrDefault();
                            code.Author = item.Author;
                            code.CommitID = item.CommitID;
                            code.GitHubUrl = item.GitHubUrl;
                            code.IssueID = item.IssueID;
                            code.LastUpdateDate = item.LastUpdateDate;
                            code.Link = item.Link;
                            code.Platform = item.Platform;
                            code.Products = item.Products;
                            code.SyncDate = DateTime.UtcNow;
                            code.Title = item.Title;
                            code.Description = item.Description;
                           
                            context.Entry<Code>(code).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            context.Codes.Add(item);
                        }
                    }
                    context.SaveChanges();
                }
            }
            return "synced!";
        }

    }
}