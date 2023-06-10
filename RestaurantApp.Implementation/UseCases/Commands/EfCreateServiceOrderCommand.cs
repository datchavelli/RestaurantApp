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
    public class EfCreateServiceOrderCommand : EfUseCase, IServiceOrderCommand
    {
        private IApplicationActor _actor;
        private CreateServiceOrderValidator _validator;
        public EfCreateServiceOrderCommand(RestaurantAppContext context, IApplicationActor actor, CreateServiceOrderValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 10;

        public string Name => "Waiter Service";

        public string Description => "UseCase for waiters for serving guests.";

        public void Execute(CreateServiceOrderDto request)
        {
            _validator.ValidateAndThrow(request);

            List<OrderItem> orderItems = new List<OrderItem>();

            if(request.MenuItems.Count() != request.MenuQuantity.Count())
            {
                throw new Exception("MenuItems and MenuQuantity are not the same lenght");
            }

            var order = Context.Orders.FirstOrDefault(x => x.Table.TableNumber == request.TableNumber);

            for(var i=0; i<request.MenuItems.Count(); i++)
            {
                var menuItemPrice = Context.MenuItems.FirstOrDefault(x => x.Id == request.MenuItems[i]).Price;

                orderItems.Add(new OrderItem
                {
                    Order = order,
                    Quatity = request.MenuQuantity[i],
                    MenuItemId = request.MenuItems[i],
                    Subtotal = request.MenuQuantity[i] * menuItemPrice
                });              
            }

            Context.AddRange(orderItems);
            Context.SaveChanges();

            

        }
    }
}
