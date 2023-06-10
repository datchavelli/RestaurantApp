using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfGetTableQuery : EfUseCase, IGetTableQuery
    {
        public EfGetTableQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Get One Table";

        public string Description => "Getting information about one table and its current orders if they exist.";

        public TableDto Execute(int search)
        {
            var query = Context.Tables.AsQueryable();

            if (search > 0)
            {
                query = query.Where(x => x.Id == search);
            }
            else
            {
                throw new Exception("Id not valid");
            }

            var result = query.FirstOrDefault();



            TableDto menuItem = new TableDto()
            {
                Id = result.Id,
                TableNumber = result.TableNumber,
                Capacity = result.Capacity,
                Status = result.Status.ToString(),
                Orders = result.Orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Waiter = o.Waiter.UserName,
                    TakenAt = o.OrderTime.ToString(),
                    Status = o.OrderStatus.ToString(),
                    ReservationId = o.ReservationId,
                    TotalAmount = o.TotalAmount,
                    OrderItems = o.Items.Select(i => new OrderItemDto
                    {
                        MenuItem = i.MenuItem.Name,
                        Price = i.MenuItem.Price,
                        Quantity = i.Quatity
                    })
                })

            };

            return menuItem;
        }
    }
}
