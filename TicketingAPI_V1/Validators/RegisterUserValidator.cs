using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingAPI_V1.Models;

namespace TicketingAPI_V1.Validators
{
    public partial class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.PhoneNumber).Must(BeAValidPhone).WithMessage("Invalid phoneNumber");
            RuleFor(x => x.Password).Length(6, 35).WithMessage("Invalid passowrd");
            RuleFor(x => x.ConfirmPassword).Length(6, 35).WithMessage("Invalid confirm password").Matches(x=>x.Password).WithMessage("Password and confirm password should match");
            RuleFor(x=>x.FirstName).Length(2,35).WithMessage("Invalid FirstName");
            RuleFor(x => x.LastName).Length(2, 35).WithMessage("Invalid LastName");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email");
        }
        private bool BeAValidPhone(int phone)
        {
            bool isOk;
            double phoneLength = Math.Floor(Math.Log10(phone) + 1);

            isOk = phoneLength == 10 ? true : false;

            return isOk;
        }
    }
}