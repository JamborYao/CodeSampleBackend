using CodeSampleBackend.ComFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace CodeSampleBackend.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private BasicCRUD dal;
        public ProductController()
        {
            dal = new BasicCRUD();
        }
        // GET api/<controller>
        public IEnumerable<Product> Get()
        {
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            return context.Products;
        }

        // GET api/<controller>/5
  
        public Product Get(int id)
        {
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();

            return context.Products.Where(c => c.id == id).FirstOrDefault();
        }

        // POST api/<controller>
        public HttpStatusCodeResult Post([FromBody]string value)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.Exception);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(" | ", errors));
            }
            List<Product> products = HttpHelper.GetProducts();
            foreach (var item in products)
            {
                dal.AddOrUpdate<Product>(item, c => c.Name == item.Name, Basic.ToDictionary<Product>(item));
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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