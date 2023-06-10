using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class CompleteOrderValidator : AbstractValidator<CompleteOrderDto>
    {
        public CompleteOrderValidator()
        {
            RuleFor(x => x.OrderStatus).NotEmpty()
                                       .When(x => string.IsNullOrEmpty(x.OrderStatus))
                                       .WithMessage("Cannot send an emtpy OrderStatus");
        }
    }
}
