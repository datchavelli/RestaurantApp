using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.MenuItem).NotEmpty()
                                    .When(x => x.MenuItem < 0)
                                    .WithMessage("MenuItem needs to be a valid number");
        }
    }
}
