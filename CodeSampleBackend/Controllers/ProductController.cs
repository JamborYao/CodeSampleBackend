using CodeSampleBackend.ComFunc;
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
    public class ProductController : ApiController
    {
        private DAL.DALProcessLog dal;
        public ProductController()
        {
            dal = new DAL.DALProcessLog();
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
        public void Post([FromBody]string value)
        {
            List<Product> products = HttpHelper.GetProducts();
            foreach (var item in products)
            {
                dal.AddOrUpdate<Product>(item, c => c.Name == item.Name, Basic.ToDictionary<Product>(item));
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