using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using TicketingAPI_V1.Models;
using TicketingAPI_V1.Repositories;

namespace TicketingAPI_V1.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        private UserRepository _userRepository;

        public AccountController()
        {
            _userRepository = new UserRepository();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Authenticate(LoginModel model)
        {
            IHttpActionResult result = null;

            BaseResult<StringIdResult> r =await  _userRepository.GetUser(model);
            result = GetResult(r);
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> CreateUser([FromBody]RegisterModel model)
        {
            IHttpActionResult result = null;

            BaseResult<StringIdResult> r = await _userRepository.CreateUser(model);
            result = GetResult<StringIdResult>(r);
            return result;
        }
    }
}
