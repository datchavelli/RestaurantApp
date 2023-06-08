using FluentValidation;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.TableNumber).NotNull()
                                       .WithMessage("Table number must be chosen");

            RuleFor(x => x.OrderStatus).NotNull()
                                       .When(x => string.IsNullOrEmpty(x.OrderStatus))
                                       .WithMessage("You need to set an OrderStatus")
                                       .When(x => Enum.IsDefined(typeof(OrderStatus), x.OrderStatus))
                                       .WithMessage("OrderStatus needs to be either 'Pending','Completed', 'Hold' or 'InProgress'");
        }
    }
}
