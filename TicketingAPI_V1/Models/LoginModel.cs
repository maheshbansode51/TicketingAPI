using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingAPI_V1.Models
{
    public class LoginModel
    {
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}