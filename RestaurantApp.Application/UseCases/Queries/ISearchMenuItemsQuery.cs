using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries
{
    public interface ISearchMenuItemsQuery : IQuery<SearchMenuItems, PagedResponse<MenuItemDto>>
    {
    }
}
