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



            var order = Context.Orders.FirstOrDefault(x => x.Table.TableNumber == request.TableNumber && x.OrderStatus != OrderStatus.Completed);

            if(request.TableNumber == 0)
            {
                throw new Exception("Table Number cannot be 0");
            }

            if(request.MenuItem == 0)
            {
                throw new Exception("Menu Item cannot be 0");
            }

            if(request.MenuQuantity == 0)
            {
                throw new Exception("Cannot select 0 items");
            }

            if(order.Items.Any(x => x.MenuItemId == request.MenuItem))
            {
                order.Items.FirstOrDefault(x => x.MenuItemId == request.MenuItem).Quantity += request.MenuQuantity;
                order.Items.FirstOrDefault(x => x.MenuItemId == request.MenuItem).Subtotal += request.MenuQuantity;
            }
            else
            {
                var menuItemPrice = Context.MenuItems.FirstOrDefault(x => x.Id == request.MenuItem).Price;

                OrderItem orderItem = new OrderItem
                {
                    Order = order,
                    Quantity = request.MenuQuantity,
                    MenuItemId = request.MenuItem,
                    Subtotal = request.MenuQuantity * menuItemPrice,
                    IsActive = true
                };

                order.TotalAmount = order.TotalAmount + (request.MenuQuantity * menuItemPrice);

                Context.Add(orderItem);
            }
            
               
            Context.SaveChanges();

            

        }
    }
}
