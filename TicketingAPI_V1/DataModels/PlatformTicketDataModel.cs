using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingAPI_V1.DataModels
{
    public class PlatformTicketDataModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string BookingDate { get; set; }
        public string TicketPrice { get; set; }
        public string NoOfPersons { get; set; }
        public string JourneyDate { get; set; }

    }
}