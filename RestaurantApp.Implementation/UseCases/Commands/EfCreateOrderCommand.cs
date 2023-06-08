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
    public class EfCreateOrderCommand : EfUseCase, ICreateOrderCommand
    {
        private IApplicationActor _actor;
        private CreateOrderValidator _validator;
        public EfCreateOrderCommand(RestaurantAppContext context, IApplicationActor actor, CreateOrderValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Creating a new Order";

        public string Description => "UseCase for creating a new order that can be done by Admin and a Waiter.";

        public void Execute(CreateOrderDto request)
        {
            _validator.ValidateAndThrow(request);


            var orderStatus = OrderStatus.Pending;

            switch(request.OrderStatus)
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
                    orderStatus= OrderStatus.Pending;
                    break;
            }

            Order order = new Order()
            {
                WaiterId = _actor.Id,
                Table = Context.Tables.FirstOrDefault(t => t.TableNumber == request.TableNumber),
                OrderStatus = orderStatus,
                TotalAmount = Context.OrderItems.Where(x => x.Order.Table.TableNumber == request.TableNumber).Sum(s => s.Subtotal),
                OrderTime = DateTime.UtcNow
            };

            Context.Add(order);
            Context.SaveChanges();
        }
    }
}
