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
            string alias = paras.Where(c => c.Key == "alias").FirstOrDefault().Value;
            var codes = dal.GetAll<Code>();
            int total;
            List<Code>  queryCodes = QueryCodeView(page,limit,product,platform,alias,out total);
            var pageview = DAL.DALGenerateView.GetCodeView(queryCodes,total);
            return Ok(pageview);
        }

        public List<Code> QueryCodeView( string pageStr, string limitStr, string product, string platform,string alias, out int total)
        {
            int page = Convert.ToInt32(pageStr);
            int limit = Convert.ToInt32(limitStr);

            IEnumerable<Code> subCode = dal.GetAll<Code>();
            if (alias != null)
            {
                subCode = from m in subCode where dal.GetEntities<CodeOwnership>(c => c.Type == "code" && c.support_alias == alias).Any(x => x.FkId == m.id) == true select m;
            }

            if (product != null)
            {
                subCode = subCode.Where(c => (c.Products != null ? c.Products.ToLower().Contains(product + ":") : false));
            }
            if (platform != null)
            {
                subCode = subCode.Where(c => (c.Platform != null ? c.Platform.ToLower().Contains(platform + ":") : false));
            }
            total = subCode.Count();
            if (page != 0 || limit != 0)
            {
                page = page == 0 ? 1 : page;
                subCode = subCode.Skip((page - 1) * limit).Take(limit);
            }

            return subCode.ToList();
        }


        public IHttpActionResult Post()
        {
            int i = 1;
            while (true)
            {
                CodeHelper helper = new CodeHelper();
                helper.GetAcomRequestBody(Constants.SampleCodeURL + i).ConvertBodyToCode(helper.Body).InsertToDatabase(helper.codes, dal);
                if (helper.Body.Contains("unable to find any sample"))
                {
                    break;
                }
                i++;
            }
            return Ok();
        }

    }
}