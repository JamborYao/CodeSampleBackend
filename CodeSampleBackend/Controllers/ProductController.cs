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
            MoonCakeCodeSampleEntities context = new MoonCakeCodeSampleEntities();
            foreach (var item in products)
            {

                if (context.Products.Any(c => c.Name == item.Name))
                {
                    var product = context.Products.Where(c => c.Name == item.Name).FirstOrDefault();
                    product.Value = item.Value;
                    context.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    context.Products.Attach(item);
                    context.Entry<Product>(item).State = System.Data.Entity.EntityState.Added;
                }
                context.SaveChanges();

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