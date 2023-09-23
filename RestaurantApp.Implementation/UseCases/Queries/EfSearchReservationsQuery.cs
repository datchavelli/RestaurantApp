using Microsoft.EntityFrameworkCore;
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
            IQueryable<Reservation> query = Context.Reservations.Include(x => x.Tables).ThenInclude(x => x.Orders).ThenInclude(x => x.Waiter);

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

            if(search.TableNumber != null)
            {
                query = query.Where(x => x.Tables.Any(t => t.TableNumber == search.TableNumber));
            }

            return query.ToPagedResponse<Reservation, ReservationDto>(search, x => new ReservationDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                Status = x.ReservationStatus.ToString(),
                GuestCount = x.GuestCount,
                ReservationDate = x.ReservationDate.ToString(),
                ReservedBy = x.Receptionist.UserName,
                Table = x.Tables.Select(t => new TableDto
                {
                    TableNumber = t.TableNumber,
                    Status = t.Status.ToString(),
                    Capacity = t.Capacity,
                    Orders = t.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        Status = o.OrderStatus.ToString(),
                        TotalAmount = o.TotalAmount,
                        TakenAt = o.OrderTime.ToString(),
                        Waiter = o.Waiter.UserName
                    })
                })
            });


        }
    }
}
