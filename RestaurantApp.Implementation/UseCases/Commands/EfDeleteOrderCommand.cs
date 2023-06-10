using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfDeleteOrderCommand : EfUseCase, IDeleteOrderCommand
    {
        private IApplicationActor _actor;
        public EfDeleteOrderCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 30;

        public string Name => "Delete Order";

        public string Description => "UseCase for removing orders.";

        public void Execute(int request)
        {
            var order = Context.Orders.Find(request);

            if(order == null)
            {
                throw new Exception("No order under that Id");
            }

            if(order.OrderStatus.ToString() != "Completed")
            {
                throw new Exception("Order is not Completed.");

            }

            order.IsActive = false;
            order.DeletedAt = DateTime.UtcNow;
            order.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
