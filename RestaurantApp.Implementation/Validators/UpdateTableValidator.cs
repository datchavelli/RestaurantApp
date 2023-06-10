using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class UpdateTableValidator : AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.TableNumber).NotNull()
                                       .When(x => x.TableNumber == 0)
                                       .WithMessage("Table number is invalid");

            RuleFor(x => x.Capacity).NotNull()
                                       .When(x => x.Capacity == 0)
                                       .WithMessage("Capacity number is invalid");
        }
    }
}
