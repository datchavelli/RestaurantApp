using RestaurantApp.Application.Queries.Searches;
using RestaurantApp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries
{
    public interface IGetStatisticsQuery : IQuery<StatisticFilterSearch, PagedResponse<StatisticsDto>>
    {
    }
}
