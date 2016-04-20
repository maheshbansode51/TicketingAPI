using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TicketingAPI_V1.Controllers
{
    [RoutePrefix("api/Ticket")]
    public class TicketController : BaseApiController
    {

        [HttpGet]
        [Route("GetAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            IHttpActionResult result = null;
            return result;
        }          

        [HttpPost]
        [Route("Book")]
        public async Task<IHttpActionResult> Book()
        {
            IHttpActionResult result = null;
            return result;
        }

        [HttpPost]
        [Route("Cancel")]
        public async Task<IHttpActionResult> Cancel()
        {
            IHttpActionResult result = null;
            return result;
        }
    }
}
