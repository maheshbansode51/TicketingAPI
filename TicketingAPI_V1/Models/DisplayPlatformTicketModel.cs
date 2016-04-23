using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingAPI_V1.DataModels;

namespace TicketingAPI_V1.Models
{
    public class DisplayPlatformTicketModel
    {
        public string Id { get; set; }       
        public string City { get; set; }
        public string State { get; set; }
        public string BookingDate { get; set; }
        public string TicketPrice { get; set; }
        public string NoOfPersons { get; set; }
        public string JourneyDate { get; set; }
        public string TicketStatus { get; set; }
    }
}