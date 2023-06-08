using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(RestaurantAppContext context)
        {

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Matches("^(?=.{3,12}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")
                .WithMessage("Invalid username format. Min 3 characters - allowed letters, digits, . and _")
                .Must(x => !context.Users.Any(u => u.UserName == x))
                .WithMessage("Username already in use.");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required.")
               .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
               .WithMessage("Password doesn't meet the complexity criteria.");
        }
    }
}
