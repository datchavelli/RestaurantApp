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
    public class EfNewOrderCommand : EfUseCase, ICreateOrderCommand
    {
        private IApplicationActor _actor;
        private NewOrderValidator _validator;
        public EfNewOrderCommand(RestaurantAppContext context, IApplicationActor actor, NewOrderValidator validator) : base(context)
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


            //Kada nove goste usluzujemo pravimo novi Order koji nema total ammount i status mu je Pending zato sto nije zavrsen.

            Order order = new Order()
            {
                WaiterId = _actor.Id,
                Table = Context.Tables.FirstOrDefault(t => t.TableNumber == request.TableNumber),
                OrderStatus = orderStatus,
                TotalAmount = 0,
                OrderTime = DateTime.UtcNow
            };

            Context.Add(order);
            Context.SaveChanges();
        }
    }
}
