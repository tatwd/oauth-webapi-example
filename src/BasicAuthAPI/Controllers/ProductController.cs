using BasicAuthAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using TModel;

namespace BasicAuthAPI.Controllers
{
    public class ProductController : ApiController
    {
        [BasicAuthentication]
        [Route("api/products")]
        public IHttpActionResult Get()
        {
            //string username = Thread.CurrentPrincipal.Identity.Name;

            using (TestDbEntities db = new TestDbEntities())
            {
                var products = db.TProducts.ToList();

                return Json(products);
            }
        }
    }
}
