using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TicketingAPI_V1.Controllers
{
    public class PlatformTicketController : BaseApiController
    {
        [HttpPost]
        [Route("BookPlatform")]
        public async Task<IHttpActionResult> BookPlatform()
        {
            IHttpActionResult result = null;
            return result;
        }
    }
}
