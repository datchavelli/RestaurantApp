using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.StartTime).NotNull()
                                     .When(x => x.StartTime < DateTime.UtcNow)
                                     .WithMessage("StartTime needs to be in the future");

            RuleFor(x => x.EndTime).NotNull()
                                     .When(x => x.EndTime < DateTime.UtcNow)
                                     .WithMessage("EndTime needs to be in the future");
        }
    }
}
