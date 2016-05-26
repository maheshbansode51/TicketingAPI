using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TicketingAPI_V1.Models;
using TicketingAPI_V1.Repositories;

namespace TicketingAPI_V1.Controllers
{
    [RoutePrefix("api/PlatformTickets")]
    public class PlatformTicketController : BaseApiController
    {
        private IPlatformTicketRepository _ticketRepository;

        public PlatformTicketController(IPlatformTicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpPost]
        [Route("{userId}/book")]
        public async Task<IHttpActionResult> BookPlatform(string userId,[FromBody]PlatformTicketModel model)
        {
            IHttpActionResult result = null;

            BaseResult<DisplayPlatformTicketModel> r = await _ticketRepository.BookPlatformTicket(userId,model);
            result = GetResult(r);

            return result;
        }
    }
}
