using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingAPI_V1.DataModels;

namespace TicketingAPI_V1.Models
{
    public class DisplayTicketModel
    {
        public string Id { get; set; }
        public string coach { get; set; }
        public int[] seatNumbers { get; set; }
        public string TrainName { get; set; }   
        public string Source { get; set; }
        public string Destination { get; set; }
        public string SourceCode { get; set; }
        public string DestinationCode { get; set; }
        public string BookingDate { get; set; }
        public string JourneyDate { get; set; }
        public string TicketPrice { get; set; }
        public string TicketStatus { get; set; }
        public string TicketType { get; set; }
    }
}