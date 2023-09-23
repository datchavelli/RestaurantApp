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
    public class EfDeleteReservationCommand : EfUseCase, IDeleteReservationCommand
    {
        private IApplicationActor _actor;
        public EfDeleteReservationCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 25;

        public string Name => "Delete Reservation";

        public string Description => "Removing Reservation. ";

        public void Execute(int request)
        {
            var reservation = Context.Reservations.Find(request);
            var table = Context.Tables.FirstOrDefault(t => t.ReservationId == request);

            if (reservation == null )
            {
                throw new Exception("Not found");
            }

            if (!reservation.IsActive)
            {
                throw new Exception("Not found");
            }

            if(reservation.ReservationStatus.ToString() != "Confirmed")
            {
                throw new Exception("Reservation is not completed!");
            }


            reservation.IsActive = false;
            reservation.DeletedAt = DateTime.UtcNow;
            reservation.DeletedBy = _actor.Username;
            table.Status = Domain.Entities.TableStatus.Available;
            table.ReservationId = null;


            /*Context.Remove(reservation);*/

            Context.SaveChanges();
        }
    }
}
