using FluentValidation;
using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfCompleteOrderCommand : EfUseCase, ICompleteOrderCommand
    {
        private IApplicationActor _actor;
        private CompleteOrderValidator _validator;

        public EfCompleteOrderCommand(RestaurantAppContext context, IApplicationActor actor, CompleteOrderValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Complete Order";

        public string Description => "Complete order does update for the status of the order and sums up the totalAmmount";

        public void Execute(CompleteOrderDto request)
        {
            _validator.ValidateAndThrow(request);

            var orderStatus = OrderStatus.Pending;

            var orderId = Context.Orders.Where(x => x.Id == request.OrderId).Select(x => x.Id).FirstOrDefault();

            if(orderId == null)
            {
                throw new Exception("Invalid OrderId");
            }

            switch (request.OrderStatus)
            {
                case "Completed":
                    orderStatus = OrderStatus.Completed;
                    break;
                case "InProgress":
                    orderStatus = OrderStatus.InProgress;
                    break;
                case "Hold":
                    orderStatus = OrderStatus.Hold;
                    break;
                default:
                    orderStatus = OrderStatus.Pending;
                    break;
            }

            var order = Context.Orders.FirstOrDefault(x => x.Id == request.OrderId);

            order.OrderStatus = orderStatus;
            order.UpdatedAt = DateTime.UtcNow;
            order.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
