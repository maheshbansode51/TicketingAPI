using FirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketingAPI_V1.Controllers
{
    public class BaseApiController : ApiController
    {      

        public IHttpActionResult GetResult<T>(BaseResult<T> item)
        {
            IHttpActionResult result = null;

            if (item.Suceeded)
            {
                result = Ok(item.Value);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(item.ErrorsString))
                {
                    result = BadRequest(item.ErrorsString);
                }
                else
                {
                    result = BadRequest();
                }
            }

            return result;
        }

       
    }
}