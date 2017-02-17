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
            var pageview = DAL.DALGenerateView.GetCodeView(dal.GetAll<Code>(), page, limit, product, platform);
            return Ok(pageview);
        }


        public IHttpActionResult Post()
        {
            int i = 130;
            while (true)
            {
                CodeHelper helper = new CodeHelper();
                helper.GetAcomRequestBody(Constants.SampleCodeURL + i).ConvertBodyToCode(helper.Body).InsertToDatabase(helper.codes, dal);
                if (helper.Body.Contains("unable to find any sample"))
                {
                    break;
                }
            }
            return Ok();
        }

    }
}