using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TModel;
using TServices;

namespace BasicTokenAuthAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("login")]
        public IHttpActionResult Get()
        {
            return Ok("Login successfully!");
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Post([FromBody]TUser user)
        {
            var isOK = UserService.Register(user);

            return isOK
                ? Ok("Register successfully!")
                : Ok("Register failed!");
            //return Ok("Register successfully!");
        }
    }
}
