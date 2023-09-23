using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfCloseTableCommand : EfUseCase, ICloseTableCommand
    {
        private IApplicationActor _actor;

        public EfCloseTableCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 31;

        public string Name => "Close Table";

        public string Description => "Restoring the table to Available status";

        public void Execute(CloseTableDto request)
        {

            var tableStatus = TableStatus.Available;


            var table = Context.Tables.FirstOrDefault(x => x.TableNumber == request.TableNumber);

            table.Status = tableStatus;
            table.ReservationId = null;
            table.UpdatedAt = DateTime.UtcNow;
            table.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
