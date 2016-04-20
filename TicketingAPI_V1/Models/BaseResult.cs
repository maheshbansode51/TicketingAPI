using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TicketingAPI_V1.Models
{
    public class BaseResult<T>
    {
        public T Value { get; set; }

        public bool Suceeded { get; set; }

        public List<string> Errors { get; set; }

        public string ErrorsString
        {
            get
            {
                return this.Errors != null && this.Errors.Count > 0 ? String.Join(", ", Errors.ToArray()) : null;
            }
        }

        public BaseResult()
        {
            Errors = new List<string>();
        }

        public void SetRequiredFieldsMissing()
        {
            Suceeded = false;
            Errors.Add("Required fields missing");
        }

        public void SetRequiredFieldsMissing(params string[] items)
        {
            Suceeded = false;
            StringBuilder s = new StringBuilder();
            foreach (var item in items)
            {
                s.AppendFormat("{0}, ", item);
            }
            string fieldNames = s.ToString().Substring(0, s.Length - 2);
            Errors.Add(String.Format("{0} missing", fieldNames));
        }

        public void AddError(string error)
        {
            Suceeded = false;
            Errors.Add(error);
        }

        public void AddError(List<string> errors)
        {
            Suceeded = false;
            Errors.AddRange(errors);
        }


    }
}