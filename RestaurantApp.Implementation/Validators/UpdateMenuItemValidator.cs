using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemDto>
    {
        public UpdateMenuItemValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                               .When(x => x.Name == null)
                               .WithMessage("Name of the product needs to be added.");

            RuleFor(x => x.Description).NotEmpty()
                                .When(x => x.Description == null)
                                .WithMessage("Description of the product needs to be added.");

            RuleFor(x => x.Price).NotEmpty()
                                 .When(x => x.Price <= 0)
                                 .WithMessage("Price needs to be a valid number.");
        }
    }
}
