using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TicketingAPI_V1.Models;
using TicketingAPI_V1.Repositories;

namespace TicketingAPI_V1.Controllers
{
    [RoutePrefix("api/Tickets")]
    public class TicketController : BaseApiController
    {
        private TicketRepository _ticketRepository;
        public TicketController()
        {
            _ticketRepository = new TicketRepository();
        }

        [HttpGet]
        [Route("{userId}/all-tickets")]
        public async Task<IHttpActionResult> GetTicketsByUserId(string userId)
        {
            IHttpActionResult result = null;
            var r = await _ticketRepository.GetTicketsByUserId(userId);
            result = GetResult(r);
            return result;
        }          

        [HttpPost]
        [Route("{userId}/Book")]
        public async Task<IHttpActionResult> Book([FromUri]string userId,[FromBody]BookTicketModel model)
        {
            IHttpActionResult result = null;
            var r = await _ticketRepository.BookTicket(userId,model);
            result = GetResult(r);
            return result;
        }

        [HttpPost]
        [Route("{userId}/Cancel/{ticketId}")]
        [ResponseType(typeof(BaseResult<StringIdResult>))]
        public async Task<IHttpActionResult> Cancel(string userId,string ticketId)
        {
            IHttpActionResult result = null;
            var r = await _ticketRepository.CancelTicket(userId,ticketId);
            result = GetResult(r);
            return result;
        }
    }
}
