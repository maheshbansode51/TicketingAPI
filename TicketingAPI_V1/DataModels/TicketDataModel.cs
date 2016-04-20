using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingAPI_V1.DataModels
{
    public class TicketDataModel
    {
        public string Id { get; set; }
        public string coach { get; set; }
        public int[] seatNumbers { get; set; }
        public string TrainName { get; set; }
        public string UserId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string SourceCode { get; set; }
        public string DestinationCode { get; set; }
        public string BookingDate { get; set; }
        public string JourneyDate { get; set; }
        public string TicketPrice { get; set; }
        public string Status { get; set; }

    }
}