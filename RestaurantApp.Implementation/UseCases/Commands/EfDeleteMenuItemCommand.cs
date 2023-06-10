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
    public class EfDeleteMenuItemCommand : EfUseCase, IDeleteMenuItemCommand
    {
        private IApplicationActor _actor;
        public EfDeleteMenuItemCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 15;

        public string Name => "Delete MenuItem";

        public string Description => "Delete Item UseCase. Only for Admins";

        public void Execute(int request)
        {
            var menuItem = Context.MenuItems.Find(request);

            if (menuItem == null)
            {
                throw new Exception("Not found");
            }

            if(!menuItem.IsActive)
            {
                throw new Exception("Already deleted");
            }

            menuItem.IsActive = false;
            menuItem.DeletedAt = DateTime.UtcNow;
            menuItem.DeletedBy = _actor.Username;

            Context.SaveChanges();

        }
    }
}
