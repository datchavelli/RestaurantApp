using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfSearchTablesQuery : ISearchTablesQuery
    {
        private readonly RestaurantAppContext _context;

        public EfSearchTablesQuery(RestaurantAppContext context)
        {
            _context = context;
        }

        public int Id => 3;

        public string Name => "Table search";

        public string Description => "Searching the tables on the restaurant";

        public IEnumerable<TableDto> Execute(TableSearch search)
        {
            var query = _context.Tables
                                .Include(x => x.Orders)
                                .Where(x => x.IsActive && x.DeletedAt == null);

            if(search.TableNumber.HasValue)
            {
                query = query.Where(x => x.TableNumber == search.TableNumber);
            }

            if(!string.IsNullOrEmpty(search.Status))
            {
                query = query.Where(x => x.Status.ToString() == search.Status);
            }

            IEnumerable<TableDto> result = query.Select(x => new TableDto
            {
                Id = x.Id,
                TableNumber = x.TableNumber,
                Status = x.Status.ToString(),
                Capacity = x.Capacity,
                Orders = x.Orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Waiter = o.Waiter.UserName,
                    TakenAt = o.OrderTime.ToString(),
                    Status = o.OrderStatus.ToString(),
                    TotalAmount = o.TotalAmount,
                    ReservationId = o.ReservationId
                })
            }).ToList();

            return result;
        }
    }
}
