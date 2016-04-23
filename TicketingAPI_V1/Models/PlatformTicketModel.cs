using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingAPI_V1.Models
{
    public class PlatformTicketModel
    {           
        public string City { get; set; }
        public string State { get; set; }
        public string BookingDate { get; set; }
        public string TicketPrice { get; set; }
        public string NoOfPersons { get; set; }
        public string JourneyDate { get; set; }
       
    }
}