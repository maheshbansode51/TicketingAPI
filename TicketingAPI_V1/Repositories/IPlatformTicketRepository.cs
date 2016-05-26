using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingAPI_V1.Models;

namespace TicketingAPI_V1.Repositories
{
    public interface IPlatformTicketRepository
    {
        Task<BaseResult<DisplayPlatformTicketModel>> BookPlatformTicket(string userId, PlatformTicketModel model);
    }
}
