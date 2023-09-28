using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application;
using RestaurantApp.Application.Queries.Searches;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
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
    public class EfGetStatisticsQuery : EfUseCase, IGetStatisticsQuery
    {
        private IApplicationActor _actor;

        public EfGetStatisticsQuery(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 33;

        public string Name => "Get Statistics";

        public string Description => "Get the statistics for the current user";

        public PagedResponse<StatisticsDto> Execute(StatisticFilterSearch search)
        {
            IQueryable<Order> query = Context.Orders.Include(x => x.Waiter).Include(x => x.Table).Where(x => x.OrderStatus == OrderStatus.Completed);
            DateTime now = DateTime.Now;
            if (search.isAdmin == "1")
            {
                if (search.Today == "1")
                {
                    query = query.Where(x => x.OrderTime > now.AddHours(-24) && x.OrderTime <= now);
                }

                if (search.LastMonth == "1")
                {
                    query = query.Where(x => x.OrderTime > now.AddMonths(-1) && x.OrderTime <= now);
                }

                return query.ToPagedResponse<Order, StatisticsDto>(search, x => new StatisticsDto
                {
                    OrderId = x.Id,
                    Date = x.OrderTime.ToString(),
                    TableNumber = x.Table.TableNumber,
                    Total = x.TotalAmount,
                    User = x.Waiter.UserName,
                });
            }
            else
            {
                if (!String.IsNullOrEmpty(search.UserName))
                {
                    query = query.Where(x => x.Waiter.UserName == search.UserName);
                }

                if (search.Today == "1")
                {
                    
                    query = query.Where(x => x.OrderTime > now.AddHours(-24) && x.OrderTime <= now);
                }

                if(search.LastMonth == "1")
                {
                    query = query.Where(x => x.OrderTime > now.AddMonths(-1) && x.OrderTime <= now);
                }

                return query.ToPagedResponse<Order, StatisticsDto>(search, x => new StatisticsDto
                {
                   OrderId = x.Id,
                   Date = x.OrderTime.ToString(),
                   TableNumber = x.Table.TableNumber,
                   Total = x.TotalAmount,
                   User = x.Waiter.UserName
                } );
            }

        }
    }
}
