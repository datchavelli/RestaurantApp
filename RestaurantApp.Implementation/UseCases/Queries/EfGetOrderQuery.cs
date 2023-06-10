using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfGetOrderQuery : EfUseCase, IGetOrderQuery
    {
        public EfGetOrderQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 28;

        public string Name => "Get one Order";

        public string Description => "Get one order information.";

        public OrderDto Execute(int search)
        {
            var query = Context.Orders.AsQueryable();

            if (search > 0)
            {
                query = query.Where(x => x.Id == search);
            }
            else
            {
                throw new Exception("Id not valid");
            }

            var result = query.FirstOrDefault();

            OrderDto order = new OrderDto()
            {
                Id = result.Id,
                Status = result.OrderStatus.ToString(),
                Waiter = result.Waiter.UserName,
                TableNumber = result.Table.TableNumber,
                TotalAmount = result.TotalAmount,
                ReservationId = result.ReservationId,
                TakenAt = result.OrderTime.ToString(),
                OrderItems = result.Items.Select(x => new OrderItemDto
                {
                    MenuItem = x.MenuItem.Name,
                    Price = x.MenuItem.Price,
                    Quantity = x.Quatity
                })
            };

            return order;
        }
    }
   }

