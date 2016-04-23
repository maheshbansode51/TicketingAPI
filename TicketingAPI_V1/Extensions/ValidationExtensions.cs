using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingAPI_V1.Extensions
{
    public static class ValidationExtensions
    {
        public static List<string> ToErrorStringList(this IList<ValidationFailure> errors)
        {
            List<string> eList = new List<string>();
            if (errors != null && errors.Count > 0)
            {
                eList.Add(String.Join(",", errors.Select(x => String.Format("Property:{0} | Value:{1} | Message:{2}", x.PropertyName, x.AttemptedValue, x.ErrorMessage))));
            }

            return eList;
        }

    }
}