using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingAPI_V1.Models;

namespace TicketingAPI_V1.Validators
{
    public partial class LoginUserValidator : AbstractValidator<LoginModel>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.PhoneNumber).Must(BeAValidPhone).WithMessage("Invalid phoneNumber");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can't empty");
        }

        private bool BeAValidPhone(int phone)
        {
            bool isOk;
            double phoneLength=Math.Floor(Math.Log10(phone) + 1);

            isOk=phoneLength == 10 ? true : false;

            return isOk;
        }
        
    }
}