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
    public class EfDeleteTableCommand : EfUseCase, IDeleteTableCommand
    {
        private IApplicationActor _actor;
        public EfDeleteTableCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 17;

        public string Name => "Delete Table";

        public string Description => "UseCase where a table is removed, in case it is needed. Only Admin can use this UseCase";

        public void Execute(int request)
        {
            var table = Context.Tables.Find(request);

            if (table == null)
            {
                throw new Exception("Not found");
            }

            if (!table.IsActive)
            {
                throw new Exception("Already deleted");
            }


            /*table.IsActive = false;
            table.DeletedAt = DateTime.UtcNow;
            table.DeletedBy = _actor.Username;*/

            Context.Remove(table);
            Context.SaveChanges();
        }
    }
}
