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
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("login")]
        [BasicAuthentication]
        public IHttpActionResult Login()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (TestDbEntities db = new TestDbEntities())
            {
                var user = db.TUsers.Where(x => x.Username == username).SingleOrDefault();
                return Json(user);
            }
        }
    }
}
