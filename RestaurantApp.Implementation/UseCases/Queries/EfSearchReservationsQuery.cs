using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfSearchReservationsQuery : EfUseCase, ISearchReservationsQuery
    {
        public EfSearchReservationsQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Search Reservations";

        public string Description => "Searching reservations UseCase";

        public PagedResponse<ReservationDto> Execute(ReservationSearch search)
        {
            IQueryable<Reservation> query = Context.Reservations;

            if(!string.IsNullOrEmpty(search.CustomerName))
            {
                query = query.Where(x => x.CustomerName == search.CustomerName);
            }

            if(search.ReservationTime != null)
            {
                query = query.Where(x => x.ReservationDate == search.ReservationTime);
            }

            if(!string.IsNullOrEmpty(search.ReservedBy))
            {
                query = query.Where(x => x.Receptionist.UserName == search.ReservedBy);
            }

            if(search.GuestCount != null)
            {
                query = query.Where(x => x.GuestCount == search.GuestCount);
            }

            if(!string.IsNullOrEmpty(search.Status))
            {
                query = query.Where(x => x.ReservationStatus.ToString() == search.Status);
            }

            return query.ToPagedResponse<Reservation, ReservationDto>(search, x => new ReservationDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                Status = x.ReservationStatus.ToString(),
                GuestCount = x.GuestCount,
                ReservationDate = x.ReservationDate.ToString(),
                ReservedBy = x.Receptionist.UserName,
                Orders = x.Orders.Select(o => new OrderDto()
                {
                    Id = o.Id,
                    Waiter = o.Waiter.UserName,
                    Status = o.OrderStatus.ToString(),
                    TakenAt = o.OrderTime.ToString(),
                    TotalAmount = o.TotalAmount
                })
            });


        }
    }
}
