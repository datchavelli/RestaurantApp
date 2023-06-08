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

            if(!string.IsNullOrEmpty(search.Waiter))
            {
                query = query.Where(x => x.Waiter.UserName == search.Waiter);
            }

            if(!string.IsNullOrEmpty(search.OrderTime))
            {
                query = query.Where(x => x.OrderTime.ToString() == search.OrderTime);
            }

            if(search.TableNumber != null)
            {
                query = query.Where(x => x.Table.TableNumber == search.TableNumber);
            }    

            if(!string.IsNullOrEmpty(search.OrderStatus))
            {
                query = query.Where(x => x.OrderStatus.ToString() == search.OrderStatus);
            }

            return query.ToPagedResponse<Order, OrderDto>(search, x => new OrderDto
            {
                Waiter = x.Waiter.UserName,
                Id = x.Id,
                TakenAt = x.CreatedAt.ToString(),
                Status = x.OrderStatus.ToString(),
                TotalAmount = x.TotalAmount,
                TableNumber = x.Table.TableNumber,
                ReservationId = x.Reservation.Id

            });
        }
    }
}
