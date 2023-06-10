using FluentValidation;
using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
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
    public class EfUpdateOrderCommand : EfUseCase, IUpdateOrderCommand
    {
        private IApplicationActor _actor;
        private UpdateOrderValidator _validator;
        public EfUpdateOrderCommand(RestaurantAppContext context, IApplicationActor actor, UpdateOrderValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Update Order";

        public string Description => "Update order usecase";

        public void Execute(UpdateOrderDto request)
        {
            _validator.ValidateAndThrow(request);

            var order = Context.Orders.Find(request.Id);

            if (order == null)
            {
                throw new Exception("Table Id is not valid");
            }
           
                
            if (string.IsNullOrEmpty(request.OrderStatus))
            {
                var orderStatus = OrderStatus.Hold;

                switch(request.OrderStatus)
                {
                    case "Pending":
                        orderStatus = OrderStatus.Pending;
                        break;
                    case "InProgress":
                        orderStatus = OrderStatus.InProgress;
                        break;
                    case "Completed":
                        orderStatus = OrderStatus.Completed;
                        break;
                    case "Hold":
                        orderStatus = OrderStatus.Hold;
                        break;
                }

                order.OrderStatus = orderStatus;
            }

            if(request.TotalAmount > 0)
            {
                order.TotalAmount = request.TotalAmount.Value;
            }

            order.UpdatedAt = DateTime.UtcNow;
            order.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
