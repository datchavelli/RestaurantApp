using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Exceptions;
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
    public class EfGetReservationQuery : EfUseCase, IGetReservationsQuery
    {
        public EfGetReservationQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 26;

        public string Name => "Get Reservation";

        public string Description => "Get Reservation info.";

        public ReservationDto Execute(int search)
        {
            var query = Context.Reservations.Include(x => x.Receptionist)
                                            .Include(x => x.Tables).ThenInclude(x => x.Orders).ThenInclude(x => x.Waiter).AsQueryable();

            if (search > 0)
            {
                var check = query.Any(x => x.Id == search);

                if (!check)
                {
                    throw new EntityNotFoundException(search, nameof(CategoryDto));
                }
            }
            else
            {
                throw new Exception("Id not valid");
            }

            var result = query.FirstOrDefault();

            ReservationDto reservation = new ReservationDto()
            {
                Id = result.Id,
                CustomerName = result.CustomerName,
                StartTime  =  result.StartTime.ToString(),
                EndTime = result.EndTime.ToString(),
                Status = result.ReservationStatus.ToString(),
                GuestCount = result.GuestCount,
                ReservationDate = result.ReservationDate.ToString(),
                ReservedBy = result.Receptionist.UserName,
                Table = result.Tables.Select(x => new TableDto
                {
                    Id = x.Id,
                    TableNumber = x.TableNumber,
                    Status = x.Status.ToString(),
                    Capacity = x.Capacity,
                    Orders = x.Orders.Select(o => new OrderDto { 
                        Id = o.Id,
                        Status = o.OrderStatus.ToString(),
                        Waiter = o.Waiter.UserName,
                        TotalAmount = o.TotalAmount,
                    })
                })
            };

            return reservation;
        }
    }
}
