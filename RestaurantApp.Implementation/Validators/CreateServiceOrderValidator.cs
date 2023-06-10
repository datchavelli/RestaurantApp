using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class CreateServiceOrderValidator : AbstractValidator<CreateServiceOrderDto>
    {
        public CreateServiceOrderValidator()
        {
            RuleFor(x => x.TableNumber).NotEmpty()
                                       .When(x => x.TableNumber == null)
                                       .WithMessage("TableNumber is required.");
        }
    }
}
