using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TModel;

namespace BasicTokenAuthAPI.Controllers
{
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is " + DateTime.Now.ToString());
        }

        [HttpGet]
        [Route("authe")]
        [Authorize]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = User.Identity;
            return Ok("Hello " + identity.Name);
        }

        [HttpGet]
        [Route("autho")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetForAuthorize()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            return Ok($"Hello {identity.Name} and your role is {string.Join(",", roles.ToList())}");
        }

        [HttpGet]
        [Route("chaims")]
        [Authorize]
        public IHttpActionResult GetUserChaims()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user = new
            {
                id = Convert.ToInt32(identity.FindFirst("Id").Value),
                username = identity.FindFirst("Username").Value,
                is_logged = true
            };

            return Json(user);
        }

    }
}
