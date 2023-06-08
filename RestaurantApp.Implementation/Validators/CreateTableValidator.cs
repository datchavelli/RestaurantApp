using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class CreateTableValidator : AbstractValidator<CreateTableDto>
    {
        public CreateTableValidator()
        {
            RuleFor(x => x.TableNumber).NotEmpty()
                                       .When(x => x.TableNumber == 0)
                                       .WithMessage("Table number is required");

            RuleFor(x => x.Capacity).NotEmpty()
                                    .When(x => x.Capacity == 0)
                                    .WithMessage("Capacity number for table is required.");
            
            
        }
    }
}
