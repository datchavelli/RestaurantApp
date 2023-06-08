using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CustomerName).NotEmpty()
                                        .When(x => string.IsNullOrEmpty(x.CustomerName))
                                        .WithMessage("Customer Name is required.");

            RuleFor(x => x.GuestCount).NotEmpty()
                                      .When(x => x.GuestCount > 0)
                                      .WithMessage("Guest Count must be more than 0");

            RuleFor(x => x.ReservationStart).NotEmpty()
                                           .When(x => string.IsNullOrEmpty(x.ReservationStart))
                                           .WithMessage("Reservation needs a date when it is.");

        }
    }
}
