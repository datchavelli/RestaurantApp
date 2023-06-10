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
    public class EfUpdateReservationCommand : EfUseCase, IUpdateReservationCommand
    {
        private IApplicationActor _actor;
        private UpdateReservationValidator _validator;

        public EfUpdateReservationCommand(RestaurantAppContext context, IApplicationActor actor, UpdateReservationValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 27;

        public string Name => "Update Reservation";

        public string Description => "Admins and Receptionist can change Reservations.";

        public void Execute(UpdateReservationDto request)
        {
            _validator.ValidateAndThrow(request);

            var reservationId = Context.Reservations.Where(x => x.Id == request.Id).Select(x => x.Id).FirstOrDefault();

            var reservation = Context.Reservations.FirstOrDefault(x => x.Id == request.Id);

            if (reservationId < 0 )
            {
                throw new Exception("Invalid reservationId");
            }

            if(request.GuestCount > 0)
            {
                reservation.GuestCount = request.GuestCount.Value;
            }

            if(!string.IsNullOrEmpty(request.CustomerName))
            {
                reservation.CustomerName = request.CustomerName;
            }

            if (!string.IsNullOrEmpty(request.ReservationStatus))
            {
                var resrvationStatus = ReservationStatus.Hold;

                switch(request.ReservationStatus)
                {
                    case "Completed":
                        resrvationStatus = ReservationStatus.Confirmed;
                        break;
                    case "Canceled":
                        resrvationStatus = ReservationStatus.Canceled;
                        break;
                    default:
                        resrvationStatus = ReservationStatus.Hold;
                        break;
                }

                reservation.ReservationStatus = resrvationStatus;
            }

            if(request.EndTime < DateTime.UtcNow)
            {
                throw new Exception("EndTime cannot be before today");
            }

            if(request.StartTime < DateTime.UtcNow)
            {
                throw new Exception("StartTime cannot be before today");
            }


            reservation.UpdatedAt = DateTime.UtcNow;
            reservation.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
