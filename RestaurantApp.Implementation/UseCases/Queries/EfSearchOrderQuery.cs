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
    public class EfSearchOrderQuery : EfUseCase, ISearchOrderQuery
    {
        public EfSearchOrderQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => "Search Order";

        public string Description => "Search Order query UseCase.";

        public PagedResponse<OrderDto> Execute(OrderSearch search)
        {
            IQueryable<Order> query = Context.Orders;

            return query.ToPagedResponse<Order, OrderDto>(search, x => new OrderDto
            {
                Waiter = x.Waiter.UserName,
                Id = x.Id,
                TakenAt = x.CreatedAt.ToString(),
                Status = x.OrderStatus.ToString(),
                TotalAmount = x.TotalAmount,
                Reservation = new ReservationDto()
                {
                    Id = x.Id,
                    ReservationDate = x.Reservation.ReservationDate.ToString(),
                    ReservedBy = x.Reservation.Receptionist.UserName,
                    GuestCount = x.Reservation.GuestCount,
                    CustomerName = x.Reservation.CustomerName,
                    StartTime = x.Reservation.StartTime.ToString()
                }

            });
        }
    }
}
