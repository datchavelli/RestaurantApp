using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfRemoveOrderCommand : EfUseCase, IRemoveOrderCommand
    {
        private IApplicationActor _actor;
        public EfRemoveOrderCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 32;

        public string Name => "Brisanje iz porudzbine";

        public string Description => "Ukoliko je dodato greškom ili se klijent predomislio";

        public void Execute(RemoveOrderItemDto request)
        {
            var orderItem = Context.OrderItems.FirstOrDefault(x => x.OrderId == request.OrderId && x.MenuItemId == request.MenuItemId);
            var order = Context.Orders.FirstOrDefault(x => x.Id == request.OrderId);
            var menuItemPrice = Context.MenuItems.FirstOrDefault(x => x.Id == request.MenuItemId).Price;

            if (orderItem == null)
            {
                throw new Exception("Not found");
            }

            /*if (orderItem.IsActive == false)
            {
                throw new Exception("Already deleted");
            }*/


            /*orderItem.IsActive = false;
            orderItem.DeletedAt = DateTime.UtcNow;
            orderItem.DeletedBy = _actor.Username;*/

            order.TotalAmount -= menuItemPrice;

            Context.Remove(orderItem);
            Context.SaveChanges();
        }
    }
}
