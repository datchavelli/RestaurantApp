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
    public class EfCreateReservationCommand : EfUseCase, ICreateReservationCommand
    {
        private IApplicationActor _actor;
        private CreateReservationValidator _validator;

        public EfCreateReservationCommand(RestaurantAppContext context,IApplicationActor actor, CreateReservationValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 6;

        public string Name => "Create Reservation";

        public string Description => "Creating new reservation. Only the Admin and the Receptionist can do this usecase";

        public void Execute(CreateReservationDto request)
        {
            _validator.ValidateAndThrow(request);


            //Format Datuma : 2023-07-09T17:30:00.000

            Reservation reservation = new Reservation()
            {
                ReceptionistId = _actor.Id,
                CustomerName = request.CustomerName,
                ReservationDate = DateTime.UtcNow,
                StartTime = DateTime.Parse(request.ReservationStart),
                ReservationStatus = ReservationStatus.Confirmed,
                GuestCount = request.GuestCount

            };

            Context.Add(reservation);
            Context.SaveChanges();
        }
    }
}
