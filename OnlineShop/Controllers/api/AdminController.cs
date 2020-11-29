using Models.Dao;
using Models.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShop.Controllers.api
{
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("api/validate")]
        public bool Validate(USER user)
        {
            return new UserDao().LoginAsync(user.UserUsername, user.UserPassword);
        }
    }
}
