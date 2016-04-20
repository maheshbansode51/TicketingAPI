﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using TicketingAPI_V1.Models;

namespace TicketingAPI_V1.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Authenticate(LoginModel model)
        {
            IHttpActionResult result = null;
            

            return result;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> CreateUser(RegisterModel model)
        {
            IHttpActionResult result = null;


            return result;
        }
    }
}